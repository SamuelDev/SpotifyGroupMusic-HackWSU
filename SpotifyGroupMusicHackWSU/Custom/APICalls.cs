using Newtonsoft.Json;
using RestSharp;
using SpotifyGroupMusicHackWSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpotifyGroupMusicHackWSU.Custom
{
    public static class APICalls
    {
        public static SpotifyUser GetCurrentSpotifyUser(string AccessToken)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("me", Method.GET);
            //request.AddUrlSegment("id", LeagueWiki.Properties.Resources.apikey);
            //client.Authenticator = new RestSharp.Authenticators.OAuth2UriQueryParameterAuthenticator(AccessToken);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            //request.AddHeader()

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            SpotifyUser user = new SpotifyUser();
            user = JsonConvert.DeserializeObject<SpotifyUser>(response.Content);

            return user;
        }

        public static SpotifyUser GetSpotifyUser(string AccessToken, string UserId)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("users/{userid}", Method.GET);
            request.AddUrlSegment("userid", UserId);
            //client.Authenticator = new RestSharp.Authenticators.OAuth2UriQueryParameterAuthenticator(AccessToken);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            //request.AddHeader()

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            SpotifyUser user = new SpotifyUser();
            user = JsonConvert.DeserializeObject<SpotifyUser>(response.Content);

            return user;
        }

        public static UserPlaylists GetUsersPlaylists(string AccessToken, string UserId, int limit)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("users/{userid}/playlists?limit={limit}", Method.GET);
            request.AddUrlSegment("userid", UserId);
            request.AddUrlSegment("limit", limit.ToString());
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            UserPlaylists playlists = new UserPlaylists();
            playlists = JsonConvert.DeserializeObject<UserPlaylists>(response.Content);

            playlists.Playlists = playlists.Playlists.Where(x => x.name != "Groupify").ToList();

            return playlists;
        }

        public static PlaylistTracks GetPlaylistTracks(string AccessToken, string UserId, string PlaylistId)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("users/{userid}/playlists/{playlistid}/tracks", Method.GET);
            request.AddUrlSegment("userid", UserId);
            request.AddUrlSegment("playlistid", PlaylistId);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            PlaylistTracks tracks = new PlaylistTracks();
            tracks = JsonConvert.DeserializeObject<PlaylistTracks>(response.Content);

            return tracks;
        }

        public static MultipleSongFeatures GetMultipleSongFeatures(string AccessToken, List<string> TrackIdList)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("audio-features?ids={idlist}", Method.GET);
            string IdCommaList = "";

            foreach (string s in TrackIdList)
            {
                IdCommaList += s.Trim() + ",";
            }
            IdCommaList = IdCommaList.TrimEnd(',');
            List<string> IdArray = new List<string>();
            IdArray = IdCommaList.Split(',').ToList();

            IdCommaList = "";
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    int index = rand.Next(IdArray.Count);
                    IdCommaList += IdArray[index] + ",";
                    IdArray.RemoveAt(index);
                }
                catch (Exception e)
                {

                }
            }
            IdCommaList = IdCommaList.TrimEnd(',');

            request.AddUrlSegment("idlist", IdCommaList);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            MultipleSongFeatures features = new MultipleSongFeatures();
            features = JsonConvert.DeserializeObject<MultipleSongFeatures>(response.Content);

            return features;
        }

        public static TracksBasedOnSeed GetPlaylistTracksByVariable(string AccessToken, int SongLimit, double Dancability, double Energy, double Speechiness, string Genres)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("recommendations?target_speechiness={speechiness}&target_energy={energy}&limit={limit}&seed_genres={genres}&target_danceability={dancability}&market=US", Method.GET);
            request.AddUrlSegment("speechiness", Speechiness.ToString());
            request.AddUrlSegment("energy", Energy.ToString());
            request.AddUrlSegment("dancability", Dancability.ToString());
            request.AddUrlSegment("limit", SongLimit.ToString());
            request.AddUrlSegment("genres", Genres);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            TracksBasedOnSeed tracks = new TracksBasedOnSeed();
            tracks = JsonConvert.DeserializeObject<TracksBasedOnSeed>(response.Content);

            return tracks;
        }

        public static ArtistList GetArtistList(string AccessToken, List<string> ArtistStringList)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("artists?ids={artistids}", Method.GET);
            string ArtistString = "";
            foreach (string s in ArtistStringList)
            {
                ArtistString += s + ",";
            }
            ArtistString = ArtistString.Trim(',');
            request.AddUrlSegment("artistids", ArtistString);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            ArtistList Artists = new ArtistList();
            Artists = JsonConvert.DeserializeObject<ArtistList>(response.Content);

            return Artists;
        }

        public static CreatedPlaylist CreateNewPlaylist(string AccessToken, string UserId, string PlaylistName)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("users/{userid}/playlists", Method.POST);
            request.AddUrlSegment("userid", UserId);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            //request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new { 
                name = PlaylistName,
                @public = true
            });            

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            CreatedPlaylist playlist = new CreatedPlaylist();
            playlist = JsonConvert.DeserializeObject<CreatedPlaylist>(response.Content);

            return playlist;            
        }

        public static TrackList GetTracks(string AccessToken, string TrackIdString)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("tracks?ids={trackids}&market=US", Method.GET);
            request.AddUrlSegment("trackids", TrackIdString.Trim(','));
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            //request.AddHeader("Content-type", "application/json");

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            TrackList tracklist = new TrackList();
            tracklist = JsonConvert.DeserializeObject<TrackList>(response.Content);

            return tracklist; 
        }

        public static AddSongs AddSongsToPlaylist(string AccessToken, string UserId, string PlaylistId, string TrackURIList)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("users/{userid}/playlists/{playlistid}/tracks?position=0&uris={urilist}", Method.POST);
            request.AddUrlSegment("userid", UserId);
            request.AddUrlSegment("playlistid", PlaylistId);
            request.AddUrlSegment("urilist", TrackURIList);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            //request.AddHeader("Content-type", "application/json");

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            AddSongs playlist = new AddSongs();
            playlist = JsonConvert.DeserializeObject<AddSongs>(response.Content);

            return playlist;
        }

        public static GenreList GetGenreList(string AccessToken)
        {
            var client = new RestClient("https://api.spotify.com/v1");
            var request = new RestRequest("recommendations/available-genre-seeds", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));

            // execute api call and deserialize the results into the object
            IRestResponse response = client.Execute(request);
            GenreList Genres = new GenreList();
            Genres = JsonConvert.DeserializeObject<GenreList>(response.Content);

            return Genres;
        }

    }
}