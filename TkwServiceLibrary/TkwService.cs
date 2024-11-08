using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TkwContract;
using TkwEF.BLL;

namespace TkwServiceLibrary
{
    public class TkwService : ITkwService
    {
        public ClubResp GetClub(int id)
        {
            Club club = null;
            using (TkwContext context = new TkwContext() )
            {
                club = context.Clubs.Where(w => w.Id == id).FirstOrDefault();
            }
            return GetClubRespByClub(club);
        }

        #region Private static

        private static ClubResp GetClubRespByClub(Club club)
        {
            return new ClubResp
            {
                Id = club.Id,
                Name = club.Name,
                Address = club.Address,
                Telefon = club.Telefon,
                Email = club.Email
            };
        }
        private static ClubRespList GetClubRespListByClub(IEnumerable<Club> clubs)
        {
            ClubRespList res = new ClubRespList();
            if (clubs == null)
                return res;
            int cnt = clubs.Count();
            for (int i = 0; i < cnt; i++)
                res.Add(GetClubRespByClub(clubs.ElementAt(i)));
            return res;
        }

        #endregion
    }
}
