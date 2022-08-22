using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_1.Models;

namespace Assignment_1.Controllers
{
    public class public class MatchDB
    {
        public List<FootBallLeague> JapanMatchDetails()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MatchDBcontext"].ConnectionString))
            {
                List<FootBallLeague> japanmatchlist = new List<FootBallLeague>();
                SqlCommand cmd = new SqlCommand("spMatachesPlayedByJapan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    japanmatchlist.Add(
                        new FootBallLeague
                        {
                            MatchID = Convert.ToInt32(dr["MatchID"]),
                            TeamName1 = Convert.ToString(dr["TeamName1"]),
                            TeamName2 = Convert.ToString(dr["TeamName2"]),
                            WinningTeam = Convert.ToString(dr["WinningTeam"]),
                            MatchStatus = Convert.ToString(dr["MatchStatus"]),
                            Points = Convert.ToInt32(dr["Points"])
                        });

                }
                return japanmatchlist;

            }
        }
        public List<FootBallLeague> WinningMatchDetails()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MatchDBcontext"].ConnectionString))
            {
                List<FootBallLeague> winninglist = new List<FootBallLeague>();
                SqlCommand cmd = new SqlCommand("spRetriveWinning", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    winninglist.Add(
                        new FootBallLeague
                        {
                            MatchID = Convert.ToInt32(dr["MatchID"]),
                            //TeamName1 = Convert.ToString(dr["TeamName1"]),
                            // TeamName2 = Convert.ToString(dr["TeamName2"]),
                            WinningTeam = Convert.ToString(dr["WinningTeam"]),
                            //MatchStatus = Convert.ToString(dr["MatchStatus"]),
                            //points = Convert.ToInt32(dr["Points"])
                        });

                }
                return winninglist;

            }
        }
        public List<FootBallLeague> GetMatchdetails()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MatchDBcontext"].ConnectionString))
            {
                List<FootBallLeague> MatchDetails = new List<FootBallLeague>();
                SqlCommand cmd = new SqlCommand("select * from FootBallLeague", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    MatchDetails.Add(
                        new FootBallLeague
                        {
                            MatchID = Convert.ToInt32(dr["MatchID"]),
                            TeamName1 = Convert.ToString(dr["TeamName1"]),
                            TeamName2 = Convert.ToString(dr["TeamName2"]),
                            WinningTeam = Convert.ToString(dr["WinningTeam"]),
                            MatchStatus = Convert.ToString(dr["MatchStatus"]),
                            Points = Convert.ToInt32(dr["Points"])
                        });

                }
                return MatchDetails;

            }
        }
        public bool AddMatchDetails(FootBallLeague Match)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MatchDBcontext"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertMatchdetails", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@id", Match.MatchID);
                cmd.Parameters.AddWithValue("@team1", Match.TeamName1);
                cmd.Parameters.AddWithValue("@team2", Match.TeamName2);
                cmd.Parameters.AddWithValue("@status", Match.MatchStatus);
                cmd.Parameters.AddWithValue("@winningteam", Match.WinningTeam);
                cmd.Parameters.AddWithValue("@points", Match.Points);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i >= 1)
                    return true;
                else
                    return false;
            }

        }
    }
    Controller : Controller
    {


        // GET: MatchDetails/Details/5
        public ActionResult Details()
        {

            MatchDB matchDB = new MatchDB();
            ModelState.Clear();
            return View(matchDB.GetMatchdetails());
        }

        // GET: MatchDetails/ winningmatchdetails
        public ActionResult winningmatchdetails()
        {
            MatchDB matchDB = new MatchDB();
            ModelState.Clear();
            return View(matchDB.WinningMatchDetails());
        }

        // GET: MatchDetails/ japanmatchdetails
        public ActionResult japanmatchdetails()
        {
            MatchDB matchDB = new MatchDB();
            ModelState.Clear();
            return View(matchDB.JapanMatchDetails());
        }
        // POST: MatchDetails/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }


}