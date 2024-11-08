using System.ServiceModel;
using System.Runtime.Serialization;
using TkwEF.Core.BLL;

namespace TkwContract
{
    [ServiceContract]
    public interface ITkwService
    {
        [OperationContract]
        ClubResp GetClub(int id);
    }

    #region Class Responce

    //[DataContract(Name = "RegisterFault", Namespace = "FaultContracts/RegisterFault")]
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public class ClubResp : IBiz
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Telefon { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
    [DataContract]
    public class ClubRespList : BindingListView<ClubResp> { }

    #endregion
}
