using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ComPro.Models;
using static ComPro.Models.Enums;

namespace ComPro.Interfaces
{
    public class SearchManager : ISearch
    {
        //ApplicationDbContext Data = new ApplicationDbContext();
        IUserProfile _userProfile = new UserProfileManager ();
        INoticeBoard _noticeBoardManager = new NoticeBoardManager ();
        IEvent _event = new EventManager();

        public IEnumerable<SearchViewModel> SearchData(string Search_Text)
        {

            var fixedInput = Regex.Replace(Search_Text, "[^a-zA-Z0-9% ._]", string.Empty);

            var splitted = fixedInput.Split(' ');

            IEnumerable<UserInfo> User = _userProfile.AllUser();
            IEnumerable<NoticeBoard> Notices = _noticeBoardManager.GetApprovedNotices();
            IEnumerable<EventViewModel> Event = _event.AllEvent();
            List<SearchViewModel> SearchResult = new List<SearchViewModel>();
            

            string ResultStirng  ;
            bool Found = false; ;


            foreach(var SearchText in splitted)
            {
                 if(SearchText!="")
                {

                    foreach (var x in User)
                    {

                        ResultStirng = null;
                        int Ranking = (int)Searching.Priority0;

                        //var model = new UserInfo();
                        var properties = x.GetType().GetProperties();
                        foreach (var item in properties)
                        {
                            var finder = item.GetValue(x, null);
                            if (finder!=null)
                            if (Matchtext(SearchText, finder.ToString()))
                            {
                                ResultStirng = ResultStirng +Helpers.Constants.Start+item.Name+Helpers.Constants.End+finder.ToString();
                                Found = true;
                                    Ranking++;
                            }

                           
                        }
                        

                        if (Found)
                        {
                            //var user2 = Data.Users.FirstOrDefault(y => y.Email == x.Email);
                            SearchResult.Add(new SearchViewModel()
                            {
                                //ResultId = user2.Id,
                                ResultId = x.Id,
                                ResultName = x.Name,
                                ResultCatagory = Helpers.Constants.UserResult.ToString(),
                                MatchedText = ResultStirng,
                                Priority = Ranking,
                            });


                            Found = false;
                            Ranking = (int)Searching.Priority0;

                        }


                    }


                    
                    foreach (var y in Notices)
                    {
                        ResultStirng = null;
                        int Ranking = (int)Searching.Priority0;
                        Found = false;

                       
                        var properties = y.GetType().GetProperties();
                        foreach (var item in properties)
                        {
                           
                            
                            var finder = item.GetValue(y, null);
                            if (finder != null)
                                if (Matchtext(SearchText, finder.ToString()))
                                {
                                    ResultStirng = ResultStirng + Helpers.Constants.Start + item.Name + Helpers.Constants.End + finder.ToString();
                                    Found = true;
                                    Ranking++;
                                }


                        }

                        
                        if (Found)
                        {
                           
                                //var user2 = Data.Users.FirstOrDefault(y => y.Email == x.Email);
                                SearchResult.Add(new SearchViewModel()
                                {
                                    //ResultId = user2.Id,
                                    ResultId = y.Id,
                                    ResultName = y.Title,
                                    ResultCatagory = Helpers.Constants.Notice.ToString(),
                                    MatchedText = ResultStirng,
                                    Priority = Ranking,
                                });
                            

                        }



                    }


                    foreach (var z in Event)
                    {
                        ResultStirng = null;
                        int Ranking = (int)Searching.Priority0;
                        Found = false;


                        var properties = z.GetType().GetProperties();
                        foreach (var item in properties)
                        {


                            var finder = item.GetValue(z, null);
                            if (finder != null)
                                if (Matchtext(SearchText, finder.ToString()))
                                {
                                    ResultStirng = ResultStirng + Helpers.Constants.Start + item.Name + Helpers.Constants.End + finder.ToString();
                                    Found = true;
                                    Ranking++;
                                }


                        }






                        if (Found)
                        {
                            SearchResult.Add(new SearchViewModel()
                            {
                               
                                ResultId = z.Id,
                                ResultName = z.EventTitel,
                                ResultCatagory = Helpers.Constants.Event.ToString(),
                                MatchedText = ResultStirng,
                                Priority = Ranking,
                            });


                        }



                    }


                }
                

            }



            List<SearchViewModel> SearchResult2 = new List<SearchViewModel>();

            foreach (var check1 in SearchResult)
            {
                int index1 = SearchResult.IndexOf(check1);

                if (check1.Priority!= (int)Searching.Priority0)
                { 
                    foreach(var check2 in SearchResult)
                    {
                        int index2 = SearchResult.IndexOf(check2);

                        if ((check1.ResultId==check2.ResultId )&& (check1.ResultCatagory==check2.ResultCatagory)&& (index1!=index2))
                        {
                            //check1.Priority = check1.Priority + (int)Searching.Priority1;
                            check1.Priority = check1.Priority + check2.Priority;
                            check2.Priority = (int)Searching.Priority0;
                            check1.MatchedText = check1.MatchedText + Helpers.Constants.Start + check2.MatchedText;
                        }

                    }

                    SearchResult2.Add(new SearchViewModel()
                    {
                        ResultId = check1.ResultId,
                        ResultName = check1.ResultName,
                        ResultCatagory = check1.ResultCatagory,
                        MatchedText = check1.MatchedText,
                        Priority= check1.Priority,
                    });
                }



            }

            var FinalsearchResult = SearchResult2.OrderByDescending(i => i.Priority);


            return FinalsearchResult;

        }

        private string textformate ()
        {
            return null;
        }

        private bool Matchtext (string SearchItem, string TextContainer)
        {
            try
            {
                

            string first = TextContainer.ToLower();
            string second = SearchItem.ToLower();

            bool result = first.Contains(second);

            return result;
            }
            catch
            {
                return false;
            }
            
        }

    }
}

