using SpotifyAPI.Web;
using Spotify_API;

// ----| SpotifyAPI Token Alabilmek İçin Gerekli Olan Bilgiler |----
string clientId = ""; // Size Ait Olan Client ID Girin
string clientSecret = ""; // Size Ait Olan Client secret Girin

var spotifyApi = new Spotify(clientId, clientSecret);

// --| Megadeth sanatçısının ID'si
var artistId = "1Yox196W7bzVNZI7RBaPnf";

// --| Sanatçının adını al ve yazdır
var artistName = await spotifyApi.GetArtistName(artistId);
Console.WriteLine($"---| {artistName} |---");

// --| Albümleri Al Ve Yazdır
var albums = await spotifyApi.GetArtistAlbums(artistId);
Console.WriteLine("\nAlbüm Listesi:");
foreach (var album in albums)
{
    Console.WriteLine($"--| {album.Name} ({album.ReleaseDate})");
}