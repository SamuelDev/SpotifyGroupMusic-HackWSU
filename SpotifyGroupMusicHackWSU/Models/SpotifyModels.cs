﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpotifyGroupMusicHackWSU.Models
{
    #region Child Classes
    public class AudioFeature
    {
        public double danceability { get; set; }
        public double energy { get; set; }
        public int key { get; set; }
        public double loudness { get; set; }
        public int mode { get; set; }
        public double speechiness { get; set; }
        public double acousticness { get; set; }
        public double instrumentalness { get; set; }
        public double liveness { get; set; }
        public double valence { get; set; }
        public double tempo { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string uri { get; set; }
        public string track_href { get; set; }
        public string analysis_url { get; set; }
        public int duration_ms { get; set; }
        public int time_signature { get; set; }
    }

    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class ExternalUrls2
    {
        public string spotify { get; set; }
    }

    public class Tracks
    {
        public string href { get; set; }
        public int total { get; set; }
    }

    public class Owner
    {
        public ExternalUrls2 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Item
    {
        public bool collaborative { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public bool @public { get; set; }
        public string snapshot_id { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Image
    {
        public object height { get; set; }
        public string url { get; set; }
        public object width { get; set; }
    }

    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    public class AddedBy
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Album
    {
        public string album_type { get; set; }
        public ExternalUrls2 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class FullAlbum
    {
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public List<Copyright> copyrights { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls2 external_urls { get; set; }
        public List<object> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class ExternalUrls3
    {
        public string spotify { get; set; }
    }

    public class Artist
    {
        public ExternalUrls3 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class FullArtist
    {
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public List<object> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }


    public class ExternalIds
    {
        public string isrc { get; set; }
    }

    public class ExternalUrls4
    {
        public string spotify { get; set; }
    }

    public class ExternalUrls5
    {
        public string spotify { get; set; }
    }

    public class LinkedFrom
    {
        public ExternalUrls5 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Track
    {
        public Album album { get; set; }
        public List<Artist> artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls4 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_playable { get; set; }
        public LinkedFrom linked_from { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class PlaylistItem
    {
        public string added_at { get; set; }
        public AddedBy added_by { get; set; }
        public bool is_local { get; set; }
        public Track track { get; set; }
    }

    public class Seed
    {
        public int initialPoolSize { get; set; }
        public int afterFilteringSize { get; set; }
        public int afterRelinkingSize { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public object href { get; set; }
    }

    public class Copyright
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Artist2
    {
        public ExternalUrls3 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
    #endregion

    #region Base Classes

    public class CreatedPlaylist
    {
        public bool collaborative { get; set; }
        public object description { get; set; }
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<object> images { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public bool @public { get; set; }
        public string snapshot_id { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
    public class ArtistList
    {
        public List<FullArtist> artists { get; set; }
    }
    public class AlbumList
    {
        public List<FullAlbum> albums { get; set; }
    }

    public class TracksBasedOnSeed
    {
        public List<Track> tracks { get; set; }
        public List<Seed> seeds { get; set; }
    }

    public class MultipleSongFeatures
    {
        [JsonProperty(PropertyName = "audio_features")]
        public List<AudioFeature> features { get; set; }
    }

    public class SpotifyUser
    {
        public string birthdate { get; set; }
        public string country { get; set; }
        public object display_name { get; set; }
        public string email { get; set; }
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string product { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class UserPlaylists
    {
        public string href { get; set; }
        [JsonProperty(PropertyName = "items")]
        public List<Item> Playlists { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    public class PlaylistTracks
    {
        public string href { get; set; }
        [JsonProperty(PropertyName = "items")]
        public List<PlaylistItem> Songs { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    public class TrackList
    {
        public List<Track> tracks { get; set; }
    }

    public class AddSongs
    {
        public string snapshot_id { get; set; }
    }

    public class GenreList
    {
        public List<string> genres { get; set; }
    }
    #endregion
}