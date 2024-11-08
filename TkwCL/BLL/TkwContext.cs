using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public class TkwContext : DbContext
    {
        #region ctor

        public TkwContext()
            : base("name=TkwContext")
        { }

        static TkwContext()
        {
            Database.SetInitializer<TkwContext>(new YourDbInitializer());
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CompGraf>()
            //    .HasOptional(s => s.CompUserRed)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            System.Diagnostics.Debug.WriteLine("Вызов OnModelCreating");
            base.OnModelCreating(modelBuilder);
        }

        #region Collections

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Poyas> Poyases { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserClub> UserClubs { get; set; }
        public DbSet<UserPoyas> UserPoyases { get; set; }
        public DbSet<UserClubRoles> UserClubRoles { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompUser> CompUsers { get; set; }
        public DbSet<VesKat> VesKates { get; set; }
        public DbSet<CompVesKat> CompVesKates { get; set; }
        public DbSet<GrafStatus> GrafStatuses { get; set; }
        public DbSet<CompGraf> CompGrafs { get; set; }
        public DbSet<AgeKat> AgeKates { get; set; }

        #endregion
    }

    public class YourDbInitializer : DropCreateDatabaseIfModelChanges<TkwContext>
    {
        protected override void Seed(TkwContext context)
        {
            _setData(context);
        }
        private void _setData(TkwContext context)
        {
            try
            {
                context.GrafStatuses.Add(new GrafStatus { Name = "Готов", Order = 1 });
                context.GrafStatuses.Add(new GrafStatus { Name = "Завершен", Order = 2 });

                context.Poyases.Add(new Poyas { Name = "10 гып", Order = 1 });
                context.Poyases.Add(new Poyas { Name = "9 гып", Order = 2 });
                context.Poyases.Add(new Poyas { Name = "8 гып", Order = 3 });
                context.Poyases.Add(new Poyas { Name = "7 гып", Order = 4 });
                context.Poyases.Add(new Poyas { Name = "6 гып", Order = 5 });
                context.Poyases.Add(new Poyas { Name = "5 гып", Order = 6 });
                context.Poyases.Add(new Poyas { Name = "4 гып", Order = 7 });
                context.Poyases.Add(new Poyas { Name = "3 гып", Order = 8 });
                context.Poyases.Add(new Poyas { Name = "2 гып", Order = 9 });
                context.Poyases.Add(new Poyas { Name = "1 гып", Order = 10 });

                context.VesKates.Add(new VesKat { BeginVes = 25, EndVes = 30 });
                context.VesKates.Add(new VesKat { BeginVes = 30, EndVes = 35 });
                context.VesKates.Add(new VesKat { BeginVes = 35, EndVes = 40 });
                context.VesKates.Add(new VesKat { BeginVes = 40, EndVes = 45 });
                context.VesKates.Add(new VesKat { BeginVes = 45, EndVes = 50 });

                context.AgeKates.Add(new AgeKat { BeginAge = 8, EndAge = 9 });
                context.AgeKates.Add(new AgeKat { BeginAge = 10, EndAge = 11 });
                context.AgeKates.Add(new AgeKat { BeginAge = 12, EndAge = 13 });
                context.AgeKates.Add(new AgeKat { BeginAge = 14, EndAge = 15 });
                context.AgeKates.Add(new AgeKat { BeginAge = 16, EndAge = 17 });

                context.Clubs.Add(new Club { Name = "Черный леопард" });

                context.Users.Add(new User { Fam = "Ахматхонов", Name = "Адам", Otch = "Магомедович", Birghtday = new DateTime(2007, 07, 25) });
                context.Users.Add(new User { Fam = "Ахматхонов", Name = "Адам", Otch = "Магомедович", Birghtday = new DateTime(2008, 10, 9) });
                context.Users.Add(new User { Fam = "Баскаков", Name = "Геннадий", Otch = "Максимович", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Белокопытов", Name = "Никита", Otch = "Васильевич", Birghtday = new DateTime(2005, 10, 5) });
                context.Users.Add(new User { Fam = "Березнер", Name = "Александр", Otch = "Игоревич", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Демурова", Name = "Елена", Otch = "Давидовна", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Жуков", Name = "Егор", Otch = "Игоревич", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Зайнулин", Name = "Оскар", Otch = "Рустамович", Birghtday = new DateTime(2008, 04, 26) });
                context.Users.Add(new User { Fam = "Зайцев", Name = "Матвей", Otch = "Олегович", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Иванова", Name = "Дарья", Otch = "Антоновна", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Илатовский", Name = "Олег", Otch = "Владимирович", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Кайцуни", Name = "София", Otch = "Армановна", Birghtday = new DateTime(2009, 03, 2) });
                context.Users.Add(new User { Fam = "Каретников", Name = "Павел", Otch = "Дмитриевич", Birghtday = new DateTime(2005, 6, 16) });
                context.Users.Add(new User { Fam = "Касимская", Name = "Аделаида", Otch = "Зауровна", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Козмулич", Name = "Матвей", Otch = "Сергеевич", Birghtday = new DateTime(2011, 9, 26) });
                context.Users.Add(new User { Fam = "Кириченко", Name = "Федор", Otch = "", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Колодочка", Name = "Илья", Otch = "Дмитриевич", Birghtday = new DateTime(2009, 9, 4) });
                context.Users.Add(new User { Fam = "Коробков", Name = "Ярослав", Otch = "Николаевич", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Кравчук", Name = "Роман", Otch = "Александрович", Birghtday = new DateTime(2008, 12, 4) });
                context.Users.Add(new User { Fam = "Крылов", Name = "Андрей", Otch = "Андреевич", Birghtday = new DateTime(2005, 9, 21) });
                context.Users.Add(new User { Fam = "Куткин", Name = "Федор", Otch = "Алексеевич", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Лаптев", Name = "Иван", Otch = "Алексанрдрович", Birghtday = new DateTime(2008, 1, 14) });
                context.Users.Add(new User { Fam = "Леванов", Name = "Владимир", Otch = "Николаевич", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Меркулов", Name = "Максим", Otch = "Андреевич", Birghtday = new DateTime(2010, 1, 3) });
                context.Users.Add(new User { Fam = "Новиков", Name = "Андрей", Otch = "Дмитриевич", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Новиков", Name = "Игорь", Otch = "Михайлович", Birghtday = new DateTime(2005, 04, 25) });
                context.Users.Add(new User { Fam = "Сухова", Name = "Анастасия", Otch = "Александровна", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Уфаев", Name = "Павел", Otch = "Максимович", Birghtday = new DateTime(2000, 1, 1) });
                context.Users.Add(new User { Fam = "Филатов", Name = "Петр", Otch = "Алексеевич", Birghtday = new DateTime(2008, 12, 20) });
                context.Users.Add(new User { Fam = "Чечурин", Name = "Матвей", Otch = "Владимирович", Birghtday = new DateTime(2008, 1, 24) });
                context.Users.Add(new User { Fam = "Швыркова", Name = "Валерия", Otch = "Данииловна", Birghtday = new DateTime(2009, 2, 10) });
                context.Users.Add(new User { Fam = "Шепляков", Name = "Николай", Otch = "Владимирович", Birghtday = new DateTime(2006, 12, 22) });
                context.Users.Add(new User { Fam = "Шишко", Name = "Алексей", Otch = "Александрович", Birghtday = new DateTime(2000, 1, 1) });
                //context.Users.Add(new User { Fam = "", Name = "", Otch = "", Birghtday = new DateTime(2000, 1, 1) });


                context.SaveChanges();

                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 1, UserId = 1 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 2 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 3 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 4 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 5 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 6 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 7 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 8 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 9 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 10 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 11 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 12 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 13 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 14 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 15 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 16 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 1, UserId = 17 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 18 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 19 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 20 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 21 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 22 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 23 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 24 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 25 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 26 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 27 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 28 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 29 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 30 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 31 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 32 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 33 });

                context.SaveChanges();

                int idClubCherLeo = context.Clubs.FirstAsync(f => f.Name == "Черный леопард").Result.Id;
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Ахматхонов" && f.Name == "Адам").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Зайнулин" && f.Name == "Оскар").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Кайцуни" && f.Name == "София").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Касимская" && f.Name == "Аделаида").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Козмулич" && f.Name == "Матвей").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Кравчук" && f.Name == "Роман").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Лаптев" && f.Name == "Иван").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Чечурин" && f.Name == "Матвей").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Швыркова" && f.Name == "Валерия").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Демурова" && f.Name == "Елена").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Жуков" && f.Name == "Егор").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Зайцев" && f.Name == "Матвей").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Коробков" && f.Name == "Ярослав").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Куткин" && f.Name == "Федор").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Новиков" && f.Name == "Андрей").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubCherLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Новиков" && f.Name == "Игорь").Result.Id });

                context.Competitions.Add(new Competition { Name = "Соревнование 1", DateBegin = new DateTime(2019, 2, 16), DateEnd = new DateTime(2019, 2, 16), Actual = true });

                context.SaveChanges();

                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 1, Ves = 26.5m, Actual = true, PoyasId = context.UserClubs.Find(1).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 2, Ves = 29, Actual = true, PoyasId = context.UserClubs.Find(2).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 3, Ves = 27.5m, Actual = true, PoyasId = context.UserClubs.Find(3).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 4, Ves = 28.3m, Actual = true, PoyasId = context.UserClubs.Find(4).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 5, Ves = 27, Actual = true, PoyasId = context.UserClubs.Find(5).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 6, Ves = 26.5m, Actual = true, PoyasId = context.UserClubs.Find(6).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 7, Ves = 28.5m, Actual = true, PoyasId = context.UserClubs.Find(7).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 8, Ves = 25.5m, Actual = true, PoyasId = context.UserClubs.Find(8).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 9, Ves = 28, Actual = true, PoyasId = context.UserClubs.Find(9).CurPoyas.Id });
                ////context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = , Ves = , Actual = true });

                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 1, Actual = true });
                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 2, Actual = true });
                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 3, Actual = true });
                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 4, Actual = true });
                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 5, Actual = true });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                new BllException("Ошибка при инициализации БД", ex);
            }
        }
        private void _setDataTest(TkwContext context)
        {
            try
            {
                context.GrafStatuses.Add(new GrafStatus { Name = "Готов", Order = 1 });
                context.GrafStatuses.Add(new GrafStatus { Name = "Завершен", Order = 2 });

                context.Poyases.Add(new Poyas { Name = "10 гып", Order = 1 });
                context.Poyases.Add(new Poyas { Name = "9 гып", Order = 2 });
                context.Poyases.Add(new Poyas { Name = "8 гып", Order = 3 });
                context.Poyases.Add(new Poyas { Name = "7 гып", Order = 4 });
                context.Poyases.Add(new Poyas { Name = "6 гып", Order = 5 });
                context.Poyases.Add(new Poyas { Name = "5 гып", Order = 6 });
                context.Poyases.Add(new Poyas { Name = "4 гып", Order = 7 });
                context.Poyases.Add(new Poyas { Name = "3 гып", Order = 8 });
                context.Poyases.Add(new Poyas { Name = "2 гып", Order = 9 });
                context.Poyases.Add(new Poyas { Name = "1 гып", Order = 10 });

                context.VesKates.Add(new VesKat { BeginVes = 25, EndVes = 30 });
                context.VesKates.Add(new VesKat { BeginVes = 30, EndVes = 35 });
                context.VesKates.Add(new VesKat { BeginVes = 35, EndVes = 40 });

                context.Clubs.Add(new Club { Name = "Черный леопард" });
                context.Clubs.Add(new Club { Name = "Клуб Ибрагимовых" });

                context.Users.Add(new User { Fam = "Ибрагимов", Name = "Рауф", Otch = "", Birghtday = new DateTime(2007, 07, 25) });
                context.Users.Add(new User { Fam = "Ибрагимова", Name = "Марина", Otch = "", Birghtday = new DateTime(2008, 04, 26) });
                context.Users.Add(new User { Fam = "Ибрагимов", Name = "Костя", Otch = "", Birghtday = new DateTime(2008, 08, 08) });
                context.Users.Add(new User { Fam = "Ибрагимова", Name = "Вика", Otch = "", Birghtday = new DateTime(2007, 10, 24) });
                context.Users.Add(new User { Fam = "Иванов", Name = "Иван", Otch = "", Birghtday = new DateTime(2008, 01, 01) });
                context.Users.Add(new User { Fam = "Петров", Name = "Петя", Otch = "", Birghtday = new DateTime(2008, 06, 01) });
                context.Users.Add(new User { Fam = "Сидоров", Name = "Сидр", Otch = "", Birghtday = new DateTime(2008, 12, 02) });
                context.Users.Add(new User { Fam = "Беркович", Name = "Берк", Otch = "", Birghtday = new DateTime(2006, 02, 23) });
                context.Users.Add(new User { Fam = "Дундуков", Name = "Дундук", Otch = "", Birghtday = new DateTime(2005, 04, 25) });

                context.SaveChanges();

                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 1, UserId = 1 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 2 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 3 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 4 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 5 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 3, UserId = 6 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 7 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 2, UserId = 8 });
                context.UserPoyases.Add(new UserPoyas { Delivery = DateTime.Now.Date, Actual = true, PoyasId = 4, UserId = 9 });

                context.SaveChanges();

                int idClubIbr = context.Clubs.FirstAsync(f => f.Name == "Клуб Ибрагимовых").Result.Id;
                int idClubLeo = context.Clubs.FirstAsync(f => f.Name == "Черный леопард").Result.Id;
                int idUserIbrRauf = context.Users.FirstAsync(f => f.Fam == "Ибрагимов" && f.Name == "Рауф").Result.Id;
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubIbr, UserId = context.Users.FirstAsync(f => f.Fam == "Ибрагимов" && f.Name == "Рауф").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubIbr, UserId = context.Users.FirstAsync(f => f.Fam == "Ибрагимова" && f.Name == "Марина").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubIbr, UserId = context.Users.FirstAsync(f => f.Fam == "Ибрагимов" && f.Name == "Костя").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubIbr, UserId = context.Users.FirstAsync(f => f.Fam == "Ибрагимова" && f.Name == "Вика").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Иванов" && f.Name == "Иван").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Петров" && f.Name == "Петя").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Сидоров" && f.Name == "Сидр").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Беркович" && f.Name == "Берк").Result.Id });
                context.UserClubs.Add(new UserClub { Begin = DateTime.Now.Date, End = null, ClubId = idClubLeo, UserId = context.Users.FirstAsync(f => f.Fam == "Дундуков" && f.Name == "Дундук").Result.Id });

                context.Competitions.Add(new Competition { Name = "Соревнование 1", DateBegin = DateTime.Now.Date.AddDays(4), DateEnd = DateTime.Now.Date.AddDays(4), Actual = true });

                context.SaveChanges();

                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 1, Ves = 26.5m, Actual = true, UserPoyasId = context.UserClubs.Find(1).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 2, Ves = 29, Actual = true, UserPoyasId = context.UserClubs.Find(2).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 3, Ves = 27.5m, Actual = true, UserPoyasId = context.UserClubs.Find(3).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 4, Ves = 28.3m, Actual = true, UserPoyasId = context.UserClubs.Find(4).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 5, Ves = 27, Actual = true, UserPoyasId = context.UserClubs.Find(5).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 6, Ves = 26.5m, Actual = true, UserPoyasId = context.UserClubs.Find(6).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 7, Ves = 28.5m, Actual = true, UserPoyasId = context.UserClubs.Find(7).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 8, Ves = 25.5m, Actual = true, UserPoyasId = context.UserClubs.Find(8).CurPoyas.Id });
                context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = 9, Ves = 28, Actual = true, UserPoyasId = context.UserClubs.Find(9).CurPoyas.Id });
                //context.CompUsers.Add(new CompUser { CompId = 1, UserClubId = , Ves = , Actual = true });

                context.CompVesKates.Add(new CompVesKat { CompId = 1, VesKatId = 1, Actual = true });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                new BllException("Ошибка при инициализации БД", ex);
            }
        }
    }
}