using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Runtime.Serialization;

namespace TkwEF.Core.Helper
{
    /// <summary>
    /// Класс для хранения настроек в XML
    /// </summary>
    public static class PropertiesXML
    {
        #region Fields

        private volatile static object _lock = new object();
        private static string name = "/properties.xml"; //имя файла
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //папка приложения
        /// <summary>
        /// Возвращает полный путь
        /// </summary>
        public static string FullName { get { return string.Format("{0}/{1}", path, name); } }
        const string RootName = "Properties";
        const string UserDef = "UserDef";
        const string PodrDef = "PodrDef";

        #endregion

        #region ctor

        static PropertiesXML()
        {

        }

        #endregion

        #region Public function

        /// <summary>
        /// проверка существования файла настроек
        /// </summary>
        public static bool FileExists
        {
            get { return File.Exists(FullName); }
        }
        /// <summary>
        /// Создание нового файла XML
        /// </summary>
        private static void CreateXmlFile(bool removeOldFile = false)
        {
            lock (_lock)
            {
                if (FileExists && removeOldFile)
                    File.Delete(FullName);
                if (!FileExists)
                {
                    //string user = _getUserElementName(byUser);
                    XDocument doc = new XDocument();
                    XElement xep = new XElement(RootName);
                    //XElement xeu = new XElement(user);
                    //xep.Add(xeu);
                    doc.Add(xep);
                    doc.Save(FullName);
                }
            }
        }

        //private static tData GetDataFromXml<tData>(tData data)
        //{
        //    lock (locker)
        //    {
        //        XDocument xDoc = XDocument.Load(FullName);
        //        PropsSkyNET result = null;
        //        if(typeof(tData).GetType() is PropsBLL)
        //        {
        //            result = data as PropsBLL;
        //        }
        //        else if (data is PropsBLL)
        //        {
        //            result = data as PropsBLL;
        //        }
        //        else if (data is PropsForm)
        //        {
        //            result = data as PropsForm;
        //        }

        //        if(result==null)
        //            throw new Exception("Не верный тип данных для работы с XML свойств программы");


        //        //tData res = Activator.CreateInstance<tData>();
        //        IEnumerable<XElement> xEls = xDoc.Element(result.Nodes[0]).Elements();
        //        for (int i = 1; i < result.Nodes.Count<string>(); i++)
        //        {
        //            xEls = xEls.Elements(result.Nodes[i]);
        //            if (xEls.Count() == 0) break;
        //        }

        //        return default(tData);
        //    }
        //}

        /// <summary>
        /// Сохранение сериализуемого объекта
        /// </summary>
        /// <typeparam name="tData">тип</typeparam>
        /// <param name="serializedObj">объект</param>
        public static void Save(int user, Control ctrl)
        {
            if (!FileExists)
                CreateXmlFile();
            PropsSkyNET data = null;
            if (ctrl is Form)
            {
                Form frm = (Form)ctrl;
                if (frm.StartPosition == FormStartPosition.Manual || frm.FormBorderStyle == FormBorderStyle.Sizable)
                    data = new PropsForm(frm);
                else
                    return;
            }
            //else if (ctrl is DataGridView)
            //{
            //    if (ctrl.FindForm() == null)
            //        return;
            //    DataGridView grd = (DataGridView)ctrl;
            //    data = new PropsGrd(grd);
            //}
            else
                return;
            lock (_lock)
            {
                XDocument xdoc;
                XElement xnamespace;
                _initElements(user, data, out xdoc, out xnamespace);
                XElement xsave = ParseToXml(data, xnamespace);
                if (xsave != null)
                {
                    XElement xeRemove = Remove(xnamespace, data);
                    xnamespace.Add(xsave);
                    xdoc.Save(FullName);
                }
            }
        }
        /// <summary>
        /// Сохранение сотрудника
        /// </summary>
        /// <param name="user"></param>
        public static void Save(int user)
        {
            _saveValue(UserDef, user);
        }
        /// <summary>
        /// Сохранение подразделения
        /// </summary>
        /// <param name="podr"></param>
        public static void Save(short podr)
        {
            _saveValue(PodrDef, podr);
        }

        /// <summary>
        /// Загрузка сериализуемого объекта
        /// </summary>
        /// <typeparam name="tData">тип</typeparam>
        /// <param name="serializedObj">объект</param>
        public static void Load(int user, Control ctrl)
        {
            if (!FileExists)
                CreateXmlFile();
            XDocument xdoc;
            XElement xnamespace;
            lock (_lock)
            {
                if (ctrl is Form)
                {
                    Form frm = (Form)ctrl;
                    PropsForm props = new PropsForm(frm);
                    _initElements(user, props, out xdoc, out xnamespace);
                    if (xnamespace == null) return;
                    XElement xe = xnamespace.Element(props.NameElement);
                    if (xe != null)
                    {
                        props = (PropsForm)ParseFromXml(xe);
                        if (frm.FormBorderStyle == FormBorderStyle.Sizable)
                            frm.Size = props.Size;
                        if (frm.StartPosition == FormStartPosition.Manual)
                        {
                            int x = Screen.PrimaryScreen.WorkingArea.Width;
                            if (x < 0) x = 0;
                            int y = Screen.PrimaryScreen.WorkingArea.Height;
                            if (y < 0) y = 0;
                            if (props.Location.X > x || props.Location.Y > y)
                                props.Location = new Point();
                            frm.Location = props.Location;
                        }
                        if (props.Grids != null && props.Grids.Count > 0)
                        {
                            DataGridView grid = null;
                            DataGridViewColumn column;
                            foreach (PropsGrd grd in props.Grids)
                            {
                                Control[] arr = frm.Controls.Find(grd.NameGrd, true);
                                if (arr.Count() == 1 && arr[0] is DataGridView)
                                    grid = (DataGridView)arr[0];
                                else
                                {
                                    if (arr.Count() == 0)
                                        continue;
                                    else
                                    {
                                        foreach (Control c in arr)
                                        {
                                            if (c is DataGridView && c.Name.Equals(grd.NameGrd))
                                            {
                                                grid = (DataGridView)c;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (grid == null) continue;
                                foreach (PropsColumn clmn in grd.Columns)
                                {
                                    column = grid.Columns[clmn.Name];
                                    if (column == null) continue;
                                    if (grid.AllowUserToOrderColumns && column.DisplayIndex != clmn.DisplayIndex)
                                        column.DisplayIndex = clmn.DisplayIndex;
                                    if (grid.AllowUserToResizeColumns && column.Width != clmn.Width)
                                        column.Width = clmn.Width;
                                }
                            }
                        }
                        if ((props?.SplitContainers?.Count ?? 0) > 0)
                        {
                            SplitContainer splt = null;
                            foreach (PropsSplitContainer s in props.SplitContainers)
                            {
                                Control[] arr = frm.Controls.Find(s.Name, true);
                                if (arr.Count() == 1 && arr[0] is SplitContainer)
                                {
                                    splt = (SplitContainer)arr[0];
                                    splt.SplitterDistance = s.Distance;
                                }
                            }
                        }
                        if ((props?.Splitters?.Count ?? 0) > 0)
                        {
                            Splitter splt = null;
                            foreach (PropsSplitter s in props.Splitters)
                            {
                                Control[] arr = frm.Controls.Find(s.Name, true);
                                if (arr.Count() == 1 && arr[0] is Splitter)
                                {
                                    splt = (Splitter)arr[0];
                                    splt.Location = new Point(s.X, s.Y);
                                }
                            }
                        }
                    }
                }
                else
                    return;
            }
        }
        /// <summary>
        /// Загрузка данных о пользователе
        /// </summary>
        /// <param name="user"></param>
        public static int? LoadUser()
        {
            if (!FileExists)
                CreateXmlFile();
            lock (_lock)
            {
                XDocument xdoc = _getDocElement();
                XElement xr = _getRoot(xdoc);
                XElement xe = xr.Element(UserDef);
                if (xe == null) return null;
                return int.Parse(xe.Value);
            }
        }
        /// <summary>
        /// Загрузка данных о пользователе
        /// </summary>
        /// <param name="user"></param>
        public static short? LoadPodr()
        {
            if (!FileExists)
                CreateXmlFile();
            lock (_lock)
            {
                XDocument xdoc = _getDocElement();
                XElement xr = _getRoot(xdoc);
                XElement xe = xr.Element(PodrDef);
                if (xe == null) return null;
                return short.Parse(xe.Value);
            }
        }

        /// <summary>
        /// Возвращает элемент xml, созданный из данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static XElement ParseToXml(PropsSkyNET data, XElement xe)
        {
            Type type = data.GetType();
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            XElement xeName = null;

            int cnt = props.Count();
            if (cnt > 1)
            {
                if (data is PropsForm)
                {
                    xeName = new XElement(data.NameElement);//Форма
                    PropsForm pf = (PropsForm)data;
                    for (int i = 0; i < cnt; i++)
                    {
                        bool adding = false;
                        XElement xeparent = new XElement(props[i].Name);
                        if (props[i].PropertyType == typeof(Point))
                        {
                            if (pf.StartPosition == FormStartPosition.Manual && pf.WindowState == FormWindowState.Normal)
                            {
                                Point p = (Point)props[i].GetValue(data, null);
                                XElement xeX = new XElement("X", p.X);
                                XElement xeY = new XElement("Y", p.Y);
                                xeparent.Add(xeX, xeY);
                            }
                            else
                            {
                                if (xe.Element(data.NameElement) != null)
                                    xeparent = xe.Element(data.NameElement).Element(props[i].Name);
                                else
                                    continue;
                            }
                            adding = true;
                        }
                        else if (props[i].PropertyType == typeof(Size))
                        {
                            if (pf.FormBorderStyle == FormBorderStyle.Sizable && pf.WindowState == FormWindowState.Normal)
                            {
                                Size s = (Size)props[i].GetValue(data, null);
                                XElement xeW = new XElement("Width", s.Width);
                                XElement xeH = new XElement("Height", s.Height);
                                xeparent.Add(xeW, xeH);
                            }
                            else
                            {
                                if (xe.Element(data.NameElement) != null)
                                    xeparent = xe.Element(data.NameElement).Element(props[i].Name);
                                else
                                    continue;
                            }
                            adding = true;
                        }
                        else if(props[i].PropertyType == typeof(List<PropsGrd>))
                        {
                            List<PropsGrd> grds = (List<PropsGrd>)(props[i].GetValue(pf, null));
                            XElement xeg = null;
                            foreach (PropsGrd grd in grds)
                            {
                                xeg = new XElement(grd.NameGrd);
                                XElement xecs, xec;
                                xecs = new XElement("Columns");
                                foreach (var column in grd.Columns)
                                {
                                    xec = new XElement(column.Name);
                                    xec.SetAttributeValue("Width", column.Width);
                                    xec.SetAttributeValue("DisplayIndex", column.DisplayIndex);
                                    xecs.Add(xec);
                                }
                                xeg.Add(xecs);
                                if (xeg != null)
                                    xeparent.Add(xeg);
                            }
                            adding = true;
                        }
                        else if(props[i].PropertyType == typeof(List<PropsSplitContainer>))
                        {
                            List<PropsSplitContainer> spltList = (List<PropsSplitContainer>)(props[i].GetValue(pf, null));
                            XElement xeg = null;
                            foreach (PropsSplitContainer splt in spltList)
                            {
                                xeg = new XElement(splt.Name);
                                xeg.SetAttributeValue("SplitterDistance", splt.Distance);
                                if (xeg != null)
                                    xeparent.Add(xeg);
                            }
                            adding = true;
                        }
                        else if(props[i].PropertyType == typeof(List<PropsSplitter>))
                        {
                            List<PropsSplitter> spltList = (List<PropsSplitter>)(props[i].GetValue(pf, null));
                            XElement xeg = null;
                            foreach (PropsSplitter splt in spltList)
                            {
                                xeg = new XElement(splt.Name);

                                XElement xec;
                                xec = new XElement("Location");
                                xec.SetAttributeValue("X", splt.X);
                                xec.SetAttributeValue("Y", splt.Y);
                                xeg.Add(xec);
                                
                                if (xeg != null)
                                    xeparent.Add(xeg);
                            }
                            adding = true;
                        }
                        if (adding)
                            xeName.Add(xeparent);
                    }
                }
                //else if (data is PropsGrd)
                //{
                //    PropsGrd pf = (PropsGrd)data;
                //    xeName = new XElement(pf.NameGrd);

                //    for (int i = 0; i < cnt; i++)
                //    {
                //        if (props[i].Name == "Columns")
                //        {
                //            XElement xecs = new XElement("Columns");
                //            List<PropsColumn> columns = (List<PropsColumn>)(props[i].GetValue(pf, null));
                //            int count = columns.Count;
                //            XElement xec;
                //            for (int y = 0; y < count; y++)
                //            {
                //                PropsColumn column = columns[y];
                //                xec = new XElement(column.Name);
                //                //xec.SetAttributeValue("Name", column.Name);
                //                xec.SetAttributeValue("Width", column.Width);
                //                xec.SetAttributeValue("DisplayIndex", column.DisplayIndex);
                //                xecs.Add(xec);
                //            }
                //            xeName.Add(xecs);
                //        }
                //    }
                //}
            }
            return xeName;
        }
        /// <summary>
        /// Возвращает экземпляр, заполненный из XML
        /// </summary>
        /// <param name="xe"></param>
        /// <returns></returns>
        public static PropsSkyNET ParseFromXml(XElement xe)
        {
            PropsForm res;
            if (xe != null && xe.Parent != null)
            {
                XElement xloc = xe.Element("Location");
                Point location = new Point(0, 0);
                Size size = new Size();
                if (xloc != null)
                {
                    XElement x, y;
                    x = xloc.Element("X");
                    y = xloc.Element("Y");
                    if (x != null && y != null)
                        location = new Point(int.Parse(x.Value), int.Parse(y.Value));
                }
                XElement xsize = xe.Element("Size");
                if (xsize != null)
                {
                    XElement w, h;
                    w = xsize.Element("Width");
                    h = xsize.Element("Height");
                    if (w != null && h != null)
                        size = new Size(int.Parse(w.Value), int.Parse(h.Value));
                }
                res = new PropsForm("", "", location, size);

                PropsGrd grd = null;
                XElement xgrids = xe.Element(PropsSkyNET.GridsRazdelName);
                if (xgrids != null)
                {
                    XElement xgrd;
                    var xgrdList = xgrids.Elements();
                    res.Grids = new List<PropsGrd>();
                    for (int i = 0; i < xgrdList.Count(); i++)
                    {
                        xgrd = xgrdList.ElementAt(i);
                        if (xgrd != null)
                        {
                            grd = new PropsGrd(xgrd);
                            res.Grids.Add(grd);
                        }
                    }
                }

                PropsSplitContainer psc = null;
                XElement xeSplitContainer = xe.Element(PropsSkyNET.SplitContainerRazdelName);
                if(xeSplitContainer != null)
                {
                    XElement xsc;
                    var xscList = xeSplitContainer.Elements();
                    res.SplitContainers = new List<PropsSplitContainer>();
                    for (int i = 0; i < xscList.Count(); i++)
                    {
                        xsc = xscList.ElementAt(i);
                        if (xsc != null)
                        {
                            psc = new PropsSplitContainer(xsc);
                            res.SplitContainers.Add(psc);
                        }
                    }
                }

                PropsSplitter ps = null;
                XElement xeSplitter = xe.Element(PropsSkyNET.SplitterRazdelName);
                if(xeSplitter != null)
                {
                    XElement xsc;
                    var xsList = xeSplitter.Elements();
                    res.Splitters = new List<PropsSplitter>();
                    for (int i = 0; i < xsList.Count(); i++)
                    {
                        xsc = xsList.ElementAt(i);
                        if (xsc != null)
                        {
                            ps = new PropsSplitter(xsc);
                            res.Splitters.Add(ps);
                        }
                    }
                }
            }
            else
                res = null;
            return res;
        }
        /// <summary>
        /// Удаляет из документа элемент и возвращает этот элемент
        /// </summary>
        /// <typeparam name="tData"></typeparam>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static XElement Remove(XElement xnamespace, PropsSkyNET data)
        {
            try
            {
                XElement xe = null;
                if (data is PropsForm)
                    xe = xnamespace.Element(data.NameElement);
                else if (data is PropsGrd)
                {
                    xe = xnamespace.Element(data.Namespace);
                    if (xe != null)
                        xe = xe.Element(((PropsGrd)data).NameGrd);
                }
                if (xe != null)
                    xe.Remove();
                return xe;
            }
            catch { return null; }
        }

        #endregion

        #region Private function

        /// <summary>
        /// Возвращает корневой элемент и дочерний документ ( по имени )
        /// </summary>
        /// <param name="user"></param>
        /// <param name="data"></param>
        /// <param name="xdoc"></param>
        /// <param name="xnamespace"></param>
        private static void _initElements(int user, PropsSkyNET data, out XDocument xdoc, out XElement xnamespace)
        {
            string byUser = _getUserElementName(user);
            xnamespace = null;
            XElement xroot = _getRootByUser(byUser, out xdoc);
            if (xroot == null)
            {
                //MailRdbs.SendMailFromRobotToRazrab(GClient.CurrentUser, "Настройки программы", string.Empty);
                //return;
                throw new PropertyXmlException("Ошибка при определении заголовка настроек");
            }
            //xnamespace = xroot.Element(data.Namespace);
            if (data is PropsForm)
            {
                xnamespace = xroot.Element(data.Namespace); // Forms
                if (xnamespace == null)
                {
                    xnamespace = new XElement(data.Namespace);
                    xroot.Add(xnamespace);
                }
            }
            //else if (data is PropsGrd)
            //{
            //    if (xroot.Element("Forms") == null || xroot.Element("Forms").Element(data.NameElement) == null)
            //        return;
            //    xnamespace = xroot.Element("Forms").Element(data.NameElement).Element(data.Namespace);
            //    if (xnamespace == null)
            //    {
            //        xroot.Element("Forms").Element(data.NameElement).Add(new XElement(data.Namespace));
            //        xnamespace = xroot.Element("Forms").Element(data.NameElement).Element(data.Namespace);
            //    }
            //}
        }
        /// <summary>
        /// Созранение настройки
        /// </summary>
        /// <param name="nameElement"></param>
        /// <param name="value"></param>
        private static void _saveValue(string nameElement, object value)
        {
            XDocument xd = _getDocElement();
            XElement xe = _getRoot(xd);
            XElement xeu = xe.Element(nameElement);
            if (xeu != null)
            {
                //if (value != int.Parse(xeu.Value))
                if (!value.Equals(xeu.Value))
                {
                    xeu.Remove();
                    xeu = new XElement(nameElement, value);
                    xe.Add(xeu);
                    xd.Save(FullName);
                }
            }
            else
            {
                xeu = new XElement(nameElement, value);
                xe.Add(xeu);
                xd.Save(FullName);
            }
        }
        /// <summary>
        /// Возвращает документ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static XDocument _getDocElement()
        {
            XDocument xdoc = null;
            try
            {
                xdoc = XDocument.Load(FullName);
            }
            catch { }
            if (xdoc == null)
            {
                CreateXmlFile(true);
                xdoc = XDocument.Load(FullName);
            }
            return xdoc;
        }
        /// <summary>
        /// Возвращает корневой элемент для сотрудника
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="byUser"></param>
        /// <returns></returns>
        private static XElement _getRoot(XDocument xdoc)
        {
            XElement xroot = xdoc.Element(RootName);
            if (xroot == null)
            {
                xroot = new XElement(RootName);
                xdoc.Add(xroot);
            }
            return xroot;
        }
        /// <summary>
        /// Возвращает корневой элемент для сотрудника
        /// </summary>
        /// <param name="root"></param>
        /// <param name="byUser"></param>
        /// <returns></returns>
        private static XElement _getRootByUser(string byUser, out XDocument xdoc)
        {
            xdoc = _getDocElement();
            XElement xr = _getRoot(xdoc);
            XElement res = xr.Element(byUser);
            if (res == null)
            {
                res = new XElement(byUser);
                xr.Add(res);
            }
            return res;
        }
        /// <summary>
        /// Возвращает название в xml сотрудника ( для корневого элемента )
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string _getUserElementName(int user) { return string.Format("User_{0}", user); }

        #endregion

        #region Сериализация / Десериализация

        /// <summary>
        /// Сериализация
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="omitXMLDeclaration">Следует ли записывать XML-объявления</param>
        /// <param name="omitXMLNamespace"></param>
        /// <returns></returns>
        private static string Serialize<T>(T obj, bool omitXMLDeclaration = true, bool omitXMLNamespace = true)
        {

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (MemoryStream memStream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = omitXMLDeclaration
                };

                using (XmlWriter writer = XmlWriter.Create(memStream, settings))
                {
                    XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                    if ((omitXMLNamespace))
                        xns.Add("", "");
                    serializer.Serialize(writer, obj, xns);
                }

                return Encoding.UTF8.GetString(memStream.ToArray());
            }
        }
        private static XElement SerializeToElement<T>(T obj, bool omitXMLDeclaration = true, bool omitXMLNamespace = true)
        {
            string xml = Serialize<T>(obj, omitXMLDeclaration, omitXMLNamespace);
            xml = xml.Substring(1);
            return XElement.Parse(xml);
        }
        /// <summary>
        /// Десериализация
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(T obj, string xml)
        {
            T result = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml.ToArray());
                memStream.Write(bytes, 0, bytes.Count());
                memStream.Seek(0, SeekOrigin.Begin);

                using (XmlReader reader = XmlReader.Create(memStream))
                {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }

        #endregion

        #region PropertyXmlException

        public sealed class PropertyXmlException : Exception
        {
            public PropertyXmlException()
            {
            }

            public PropertyXmlException(string message) : base(message)
            {
            }

            public PropertyXmlException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        #endregion
    }

    #region Properties classes

    /// <summary>
    /// Абстрактный класс для свойств
    /// </summary>
    [Serializable]
    public abstract class PropsSkyNET
    {
        #region Const

        public const string FormsRazdelName = "Forms";
        public const string GridsRazdelName = "Grids";
        //const string _panelRazdelName = "Panel";
        public const string SplitterRazdelName = "Splitters";
        public const string SplitContainerRazdelName = "SplitContainers";

        #endregion

        #region Fields

        /// <summary>
        /// Возвращает название элемента
        /// </summary>
        public string NameElement { get; set; }
        public string Namespace { get; set; } 

        public string NameFormsRazdel { get { return FormsRazdelName; } }
        public string NameGridsRazdel { get { return GridsRazdelName; } }
        //public string NamePanelRazdel { get { return _panelRazdelName; } }
        public string NameSplitterRazdel { get { return SplitterRazdelName; } }
        public string NameSplitContainerRazdel { get { return SplitContainerRazdelName; } }

        #endregion

        #region ctor

        protected PropsSkyNET() { }

        protected PropsSkyNET(Control ctrl)
        {
            if (ctrl == null)
                throw new ArgumentNullException("Аргумент контрола не определен");
            _fillNameElement(ctrl);
        }

        protected PropsSkyNET(string name, string nspace)
        {
            this.NameElement = name;
            this.Namespace = nspace;
        }

        #endregion

        private void _fillNameElement(Control data)
        {
            Type type = data.GetType();
            if (data is Form)
            {
                Namespace = NameFormsRazdel;//type.Namespace; из-за Dotsfuscator
                NameElement = data.Name;
            }
            else if (data is DataGridView)
            {
                Namespace = NameGridsRazdel;
                NameElement = data.FindForm().Name;
            }
            //else if(data is Panel || data is FlowLayoutPanel || data is TableLayoutPanel)
            //{
            //    Namespace = NamePanelRazdel;
            //    NameElement = data.FindForm().Name;
            //}
            else if(data is SplitContainer)
            {
                Namespace = NameSplitContainerRazdel;
                NameElement = data.FindForm().Name;
            }
            else if(data is Splitter)
            {
                Namespace = NameSplitterRazdel;
                NameElement = data.FindForm().Name;
            }
            else
                throw new Exception("Неучтенный тип для сохранения свойств");
        }
    }
    /// <summary>
    /// Свойства для формы
    /// </summary>
    [Serializable]
    public class PropsForm : PropsSkyNET
    {
        #region Fields

        public bool Modal { get; private set; }
        public FormBorderStyle FormBorderStyle { get; set; }
        public FormStartPosition StartPosition { get; set; }
        public FormWindowState WindowState { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }

        public List<PropsGrd> Grids { get; set; }
        public List<PropsSplitContainer> SplitContainers { get; set; }
        public List<PropsSplitter> Splitters { get; set; }

        #endregion

        #region ctor

        public PropsForm() { }
        public PropsForm(Form frm)
            : base(frm)
        {
            this.Location = frm.Location; this.Size = frm.Size; this.Modal = frm.Modal; this.FormBorderStyle = frm.FormBorderStyle;
            this.StartPosition = frm.StartPosition;
            this.WindowState = frm.WindowState;
            //20170613
            //this.Grids = _getGrids(frm);
            //this.SplitContainers = _getSplits(frm);
            Grids = new List<PropsGrd>();
            SplitContainers = new List<PropsSplitContainer>();
            Splitters = new List<PropsSplitter>();
            _fillControlList(frm);
        }

        //public PropsForm(Form frm) : base(frm) { this.Location = frm.Location; this.Size = frm.Size; }
        //public PropsForm(string name, string nspace, Point location, Size size, bool modal, FormBorderStyle borderStyle, FormStartPosition startPosition)
        public PropsForm(string name, string nspace, Point location, Size size)
            : base(name, nspace)
        {
            this.Location = location;
            this.Size = size;
        }

        #endregion

        //20170613
        //private List<PropsGrd> _getGrids(Control ctrl)
        //{
        //    List<PropsGrd> res = new List<PropsGrd>();
        //    foreach (Control c in ctrl.Controls)
        //    {
        //        if (!(c is DataGridView) && c.HasChildren)
        //            res.AddRange(_getGrids(c));
        //        else
        //        {
        //            if (c is DataGridView)
        //                res.Add(new PropsGrd((DataGridView)c));
        //        }
        //    }
        //    return res;
        //}
        //20170613
        /// <summary>
        /// Заполнение списков контролов
        /// </summary>
        /// <param name="ctrl"></param>
        private void _fillControlList(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c is DataGridView)
                {
                    Grids.Add(new PropsGrd((DataGridView)c));
                }
                else if (c is SplitContainer)
                {
                    SplitContainers.Add(new PropsSplitContainer((SplitContainer)c));
                    _fillControlList(c);
                }
                else if (c is Splitter)
                {
                    Splitters.Add(new PropsSplitter((Splitter)c));
                    _fillControlList(c);
                }
                else
                    _fillControlList(c);
            }
        }
    }
    /// <summary>
    /// Сохранение настроек для DataGridView
    /// </summary>
    [Serializable]
    public class PropsColumn : PropsSkyNET
    {
        public PropsColumn(DataGridViewColumn clmn)
        {
            Name = clmn.Name;
            Width = clmn.Width;
            DisplayIndex = clmn.DisplayIndex;
        }
        public PropsColumn(XElement clmn)
        {
            Name = clmn.Name.ToString();
            if (clmn.Attribute("DisplayIndex") != null)
                DisplayIndex = int.Parse(clmn.Attribute("DisplayIndex").Value);
            if (clmn.Attribute("DisplayIndex") != null)
                Width = int.Parse(clmn.Attribute("Width").Value);
        }
        public String Name { get; set; }         // имя столбца
        public Int32 Width { get; set; }         // ширина столбца
        public Int32 DisplayIndex { get; set; }  // порядок расположения
    }

    public class PropsGrd : PropsSkyNET
    {
        public string NameGrd { get; set; }
        public List<PropsColumn> Columns { get; set; }

        public PropsGrd(DataGridView grd)
            : base(grd)
        {
            NameGrd = grd.Name;
            Columns = new List<PropsColumn>();
            foreach (DataGridViewColumn column in grd.Columns)
                Columns.Add(new PropsColumn(column));
        }

        public PropsGrd(XElement grid)
            : base(grid.Name.ToString(), grid.Parent.Name.ToString())
        {
            NameGrd = grid.Name.ToString();
            this.Columns = new List<PropsColumn>();
            XElement xclmns, xclmn;
            PropsColumn clmn;
            xclmns = grid.Element("Columns");
            if (xclmns != null)
            {
                var clmnList = xclmns.Elements();
                for (int y = 0; y < clmnList.Count(); y++)
                {
                    xclmn = clmnList.ElementAt(y);
                    clmn = new PropsColumn(xclmn);
                    this.Columns.Add(clmn);
                }
            }
        }
    }
    /// <summary>
    /// Сохранение настроек для SplitContainer
    /// </summary>
    [Serializable]
    public class PropsSplitContainer : PropsSkyNET
    {
        public PropsSplitContainer(SplitContainer split)
        {
            Name = split.Name;
            Distance = split.SplitterDistance;
        }
        public PropsSplitContainer(XElement panel)
        {
            Name = panel.Name.ToString();
            if (panel.Attribute("SplitterDistance") != null)
                Distance = int.Parse(panel.Attribute("SplitterDistance").Value);
        }
        public string Name { get; set; }         // имя контейнера
        public int Distance { get; set; }         // Положение разделителя
    }
    /// <summary>
    /// Сохранение настроек для SplitContainer
    /// </summary>
    [Serializable]
    public class PropsSplitter : PropsSkyNET
    {
        public PropsSplitter(Splitter split)
        {
            Name = split.Name;
            X = split.Location.X;
            Y = split.Location.Y;
        }
        public PropsSplitter(XElement panel)
        {
            Name = panel.Name.ToString();
            var loc = panel.Element("Location");
            if(loc != null)
            {
                if (loc.Attribute("X") != null)
                    X = int.Parse(loc.Attribute("X").Value);
                if (loc.Attribute("Y") != null)
                    Y = int.Parse(loc.Attribute("Y").Value);
            }
        }
        public string Name { get; set; }         // имя контейнера
        public int X { get; set; }         // Положение разделителя
        public int Y { get; set; }         // Положение разделителя
    }

    #endregion
}
