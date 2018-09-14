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

           
           // var fixedInput = Regex.Replace(Search_Text, "[^a-zA-Z0-9% ._]", string.Empty);

            //var splitted = fixedInput.Split(' ');
            var splitted = Search_Text.Split('-');

            IEnumerable<UserInfo> User = _userProfile.AllUser();
            IEnumerable<NoticeBoard> Notices = _noticeBoardManager.GetApprovedNotices();
            IEnumerable<EventViewModel> Event = _event.AllEvent();
            List<SearchViewModel> SearchResult = new List<SearchViewModel>();
            

            string ResultStirng  ;
            bool Found = false; 


            foreach(var SearchText in splitted)
            {
                 if(SearchText!="" && SearchText.Length>2)
                {
                   

                    foreach (var x in User)
                    {
                        Found = false;
                        ResultStirng = null;
                        int Ranking = (int)Searching.Priority0;

                        if (Matchtext(SearchText, x.Name))
                        {

                            if (!(Matchtext(ResultStirng, "Name" + Helpers.Constants.End + x.Name + Helpers.Constants.Start)))
                            {
                                ResultStirng = ResultStirng + "Name" + Helpers.Constants.End + x.Name + Helpers.Constants.Start;
                            }

                            Found = true;
                            Ranking++;
                        }

                        if (Matchtext(SearchText, x.CompanyName))
                        {

                            if (!(Matchtext(ResultStirng, "Company Name" + Helpers.Constants.End + x.CompanyName + Helpers.Constants.Start)))
                            {
                                ResultStirng = ResultStirng + "Company Name" + Helpers.Constants.End + x.CompanyName + Helpers.Constants.Start;
                            }

                            Found = true;
                            Ranking++;
                        }

                        if (Matchtext(SearchText, x.CurrentJobTitle))
                        {

                            if (!(Matchtext(ResultStirng, "Current Job Title" + Helpers.Constants.End + x.CurrentJobTitle + Helpers.Constants.Start)))
                            {
                                ResultStirng = ResultStirng + "Current Job Title" + Helpers.Constants.End + x.CurrentJobTitle + Helpers.Constants.Start;
                            }

                            Found = true;
                            Ranking++;
                        }

                        if (Matchtext(SearchText, x.Skills))
                        {

                            if (!(Matchtext(ResultStirng, "Skills" + Helpers.Constants.End + x.Skills + Helpers.Constants.Start)))
                            {
                                ResultStirng = ResultStirng + "Skills" + Helpers.Constants.End + x.Skills + Helpers.Constants.Start;
                            }

                            Found = true;
                            Ranking++;
                        }

                        string Describe = "Name : " + x.Name + " Work at : " + x.CompanyName + " Skilled on : " + x.Skills;
                        if (Found)
                        {
                           
                            SearchResult.Add(new SearchViewModel()
                            {
                               
                                ResultId = x.Id,
                                ResultName = x.Name,
                                Description = Describe,
                                ResultCatagory = Helpers.Constants.UserResult.ToString(),
                                MatchedText = ResultStirng.Remove(ResultStirng.Length - 1),
                                Priority = Ranking,
                                SearchText= Search_Text,
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


                        

                        string[] SingleSentence = y.Title.Split('!', '.', '?');
                        if(SingleSentence.Length !=0)
                        {
                        SingleSentence[0] = y.Title;
                        }
                        else
                        {
                            SingleSentence = SingleSentence.Take(SingleSentence.Count() - 1).ToArray();
                        }
                        

                        foreach (var sentence in SingleSentence)
                        {
                            if (Matchtext(SearchText, sentence))
                            {
                                if (!(Matchtext(ResultStirng, sentence)))
                                {
                                    ResultStirng = ResultStirng + sentence + Helpers.Constants.Start;
                                }
                                Found = true;
                                Ranking++;
                            }

                        }

                       
                        SingleSentence = y.Description.Split('!', '.', '?');
                        SingleSentence = SingleSentence.Take(SingleSentence.Count() - 1).ToArray();

                        foreach (var sentence in SingleSentence)
                        {
                            if (Matchtext(SearchText, sentence))
                            {
                                if (!(Matchtext(ResultStirng,sentence)))
                                {
                                    ResultStirng = ResultStirng + sentence + Helpers.Constants.Start;
                                }
                                Found = true;
                                Ranking++;
                            }
                        }

                           


                        if (Found)
                        {
                           
                                SearchResult.Add(new SearchViewModel()
                                {
                                   
                                    ResultId = y.Id,
                                    ResultName = y.Title,
                                    Description = y.Description,
                                    ResultCatagory = Helpers.Constants.Notice.ToString(),
                                    MatchedText = ResultStirng,
                                    Priority = Ranking,
                                    SearchText= Search_Text,
                                });

                            Found = false;
                            Ranking = (int)Searching.Priority0;

                        }



                    }

                   
                    foreach (var z in Event)
                    {
                        ResultStirng = null;
                        int Ranking = (int)Searching.Priority0;
                        Found = false;

                        string[] SingleSentence = z.EventTitel.Split('!', '.', '?');

                        if (SingleSentence.Length != 0)
                        {
                            SingleSentence[0] = z.EventTitel;
                        }
                        else
                        {
                            SingleSentence = SingleSentence.Take(SingleSentence.Count() - 1).ToArray();
                        }

                        foreach (var sentence in SingleSentence)
                        {
                            if (Matchtext(SearchText, sentence))
                            {
                                if (!(Matchtext(ResultStirng, sentence)))
                                {
                                    ResultStirng = ResultStirng + sentence + Helpers.Constants.Start;
                                }
                                Found = true;
                                Ranking++;
                            }
                        }

                       
                        SingleSentence = z.Description.Split('!', '.', '?');
                        SingleSentence = SingleSentence.Take(SingleSentence.Count() - 1).ToArray();

                        foreach (var sentence in SingleSentence)
                        {
                            if (Matchtext(SearchText, sentence))
                            {
                                if (!(Matchtext(ResultStirng,  sentence )))
                                {
                                    ResultStirng = ResultStirng + sentence + Helpers.Constants.Start;
                                }
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
                                Description= z.Description,
                                ResultCatagory = Helpers.Constants.Event.ToString(),
                                MatchedText = ResultStirng,
                                Priority = Ranking,
                                SearchText = Search_Text,
                            });

                            Found = false;
                            Ranking = (int)Searching.Priority0;
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
                            if (!(Matchtext(check1.MatchedText, check2.MatchedText)))
                            { check1.MatchedText = check1.MatchedText + Helpers.Constants.Start + check2.MatchedText; }
                                
                        }

                    }

                    SearchResult2.Add(new SearchViewModel()
                    {
                        ResultId = check1.ResultId,
                        ResultName = check1.ResultName,
                        ResultCatagory = check1.ResultCatagory,
                        MatchedText = check1.MatchedText,
                        Priority = check1.Priority,
                        SearchText = check1.SearchText,
                        Description=check1.Description,
                    });
                }



            }

            var FinalsearchResult = SearchResult2.OrderByDescending(i => i.Priority);


            return FinalsearchResult;

        }

       
        private bool Matchtext (string SearchItem, string TextContainer)
        {
            try
            {
                string first = null;
                string second = null;

              if(TextContainer.Length>=SearchItem.Length)
                {
               first = TextContainer.ToLower();
                  second = SearchItem.ToLower();
                }
                else
                {
                     second = TextContainer.ToLower();
                      first= SearchItem.ToLower();
                }

               first = Regex.Replace(first, "[^a-zA-Z0-9% ._]", string.Empty);

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

