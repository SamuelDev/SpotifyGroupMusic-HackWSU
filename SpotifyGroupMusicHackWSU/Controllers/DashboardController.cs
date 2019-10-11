using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using SpotifyGroupMusicHackWSU.Models;
using SpotifyGroupMusicHackWSU.Custom;

namespace SpotifyGroupMusicHackWSU.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(object access_token)
        {
            return View();
        }

        public JsonResult GetUser(string AccessToken, string TokenType, string ExpiresIn, string State)
        {
            SpotifyUser user = APICalls.GetCurrentSpotifyUser(AccessToken);

            return Json(new { Username = user.display_name, Id = user.id, Email = user.email, Images = user.images, Url = user.external_urls.spotify,
                            Birthday = user.birthdate, Country = user.country, Followers = user.followers });
        }

        public JsonResult CraftPlaylist(string AccessToken, string NameString)
        {
            List<string> NameList = NameString.Trim().Trim(',').Split(',').ToList();

            double TotalDancability = 0.0;
            int DancabilityCount = 0;
            double TotalEnergy = 0.0;
            int EnergyCount = 0;
            double TotalSpeechiness = 0.0;
            int SpeechinessCount = 0;
            SortedDictionary<string, int> Genres = new SortedDictionary<string, int>();

            List<string> ArtistIds = new List<string>();

            foreach(string name in NameList)
            {
                UserPlaylists playlists = APICalls.GetUsersPlaylists(AccessToken, name, 10);
                List<string> TrackIds = new List<string>();

                Random rand = new Random();
                playlists.Playlists = playlists.Playlists.OrderBy(x=>rand.Next()).Take(3).ToList();

                foreach (Item pl in playlists.Playlists)
                {
                    PlaylistTracks tracks = APICalls.GetPlaylistTracks(AccessToken, name, pl.id);
                    if (tracks.Songs != null)
                    {
                        foreach (PlaylistItem pli in tracks.Songs)
                        {
                            if (pli.track.id != null) 
                                TrackIds.Add(pli.track.id);
                            if(pli.track.artists[0].id != null)
                                if (!ArtistIds.Contains(pli.track.artists[0].id))
                                    ArtistIds.Add(pli.track.artists[0].id);
                        }
                    }
                }

                for (int i = 0; i <= ArtistIds.Count / 20; i++)
                {
                    
                    ArtistList artists = APICalls.GetArtistList(AccessToken, ArtistIds.Take(20).ToList());
                    if (artists.artists == null || artists.artists.Count == 0)
                        break;

                    foreach (FullArtist fa in artists.artists)
                    {
                        foreach (object o in fa.genres)
                        {
                            if (Genres.ContainsKey(o.ToString()))
                            {
                                Genres[o.ToString()]++;
                            }
                            else
                            {
                                Genres.Add(o.ToString(), 1);
                            }
                        }
                    }
                    try
                    {
                        for (int z = 1; z <= 20; z++)
                        {
                            ArtistIds.RemoveAt(0);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    
                }

                MultipleSongFeatures features = APICalls.GetMultipleSongFeatures(AccessToken, TrackIds);
                foreach (AudioFeature af in features.features)
                {
                    TotalDancability += af.danceability;
                    DancabilityCount++;
                    TotalEnergy += af.energy;
                    EnergyCount++;
                    TotalSpeechiness += af.speechiness;
                    SpeechinessCount++; 
                }
            }

            var sortedDict = Genres.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
            string GenreString = "";
            foreach (KeyValuePair<string, int> o in sortedDict)
            {
                GenreString += o.Key + ",";
            }
            GenreString = GenreString.Trim(',');
            double AvgDancability = TotalDancability / DancabilityCount;
            double AvgEnergy = TotalEnergy / EnergyCount;
            double AvgSpeechiness = TotalSpeechiness / SpeechinessCount;

            TracksBasedOnSeed SeededTracks = APICalls.GetPlaylistTracksByVariable(AccessToken, 40, AvgDancability, AvgEnergy, AvgSpeechiness, GenreString);

            return Json(new { Tracks = SeededTracks, AvgDancability = AvgDancability, AvgEnergy = AvgEnergy, AvgSpeechiness = AvgSpeechiness, Genres = GenreString.Replace(",", ", ") });
        }

        public JsonResult CreateCustomPlaylist(string AccessToken, string UserId, string TrackStringList, string PlaylistName)
        {
            CreatedPlaylist playlist = APICalls.CreateNewPlaylist(AccessToken, UserId, PlaylistName);

            TrackList tracklist = APICalls.GetTracks(AccessToken, TrackStringList);

            string URIString = "";

            foreach (Track t in tracklist.tracks)
            {
                URIString += t.uri + ",";
            }
            URIString = URIString.Trim(',');

            AddSongs added = APICalls.AddSongsToPlaylist(AccessToken, UserId, playlist.id, URIString);
            return Json(new { PlaylistId = playlist.id, TrackIdList = TrackStringList.Trim(',') });
        }

        public JsonResult GetGenreList(string AccessToken)
        {
            GenreList genres = APICalls.GetGenreList(AccessToken);
            return Json(new { genres = genres });
        }

        public JsonResult CraftPlaylistFromGenres(string AccessToken, string NameString, string[] GenreStringList)
        {
            List<string> NameList = NameString.Trim().Trim(',').Split(',').ToList();

            double TotalDancability = 0.0;
            int DancabilityCount = 0;
            double TotalEnergy = 0.0;
            int EnergyCount = 0;
            double TotalSpeechiness = 0.0;
            int SpeechinessCount = 0;
            //SortedDictionary<string, int> Genres = new SortedDictionary<string, int>();

            List<string> ArtistIds = new List<string>();

            foreach (string name in NameList)
            {
                UserPlaylists playlists = APICalls.GetUsersPlaylists(AccessToken, name, 10);
                List<string> TrackIds = new List<string>();

                Random rand = new Random();
                playlists.Playlists = playlists.Playlists.OrderBy(x => rand.Next()).Take(3).ToList();

                foreach (Item pl in playlists.Playlists)
                {
                    PlaylistTracks tracks = APICalls.GetPlaylistTracks(AccessToken, name, pl.id);
                    if (tracks.Songs != null)
                    {
                        foreach (PlaylistItem pli in tracks.Songs)
                        {
                            if (pli.track.id != null)
                                TrackIds.Add(pli.track.id);
                            if (pli.track.artists[0].id != null)
                                if (!ArtistIds.Contains(pli.track.artists[0].id))
                                    ArtistIds.Add(pli.track.artists[0].id);
                        }
                    }
                }

                MultipleSongFeatures features = APICalls.GetMultipleSongFeatures(AccessToken, TrackIds);
                foreach (AudioFeature af in features.features)
                {
                    TotalDancability += af.danceability;
                    DancabilityCount++;
                    TotalEnergy += af.energy;
                    EnergyCount++;
                    TotalSpeechiness += af.speechiness;
                    SpeechinessCount++;
                }
            }

            //var sortedDict = Genres.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
            string GenreString = "";
            foreach (string o in GenreStringList)
            {
                GenreString += o + ",";
            }
            GenreString = GenreString.Trim(',');
            double AvgDancability = TotalDancability / DancabilityCount;
            double AvgEnergy = TotalEnergy / EnergyCount;
            double AvgSpeechiness = TotalSpeechiness / SpeechinessCount;

            TracksBasedOnSeed SeededTracks = APICalls.GetPlaylistTracksByVariable(AccessToken, 40, AvgDancability, AvgEnergy, AvgSpeechiness, GenreString);

            return Json(new { Tracks = SeededTracks, AvgDancability = AvgDancability, AvgEnergy = AvgEnergy, AvgSpeechiness = AvgSpeechiness, Genres = GenreString.Replace(",", ", ") });
        }
    }
}