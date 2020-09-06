# MyAnimeList.API

<table>
<tbody>
<tr>
<td><a href="#malapiclient">MALApiClient</a></td>
</tr>
</tbody>
</table>

### MyAnimeList.API.DTOs.Requests.GetAnimeListRequest.Fields

An array containing the fields to be returned. Leave empty, for default fields.

#### Remarks

To use this property you must create a class that can map the new properties and use it in //TODO: Custom method.

### MyAnimeList.API.DTOs.Requests.GetAnimeListRequest.Limit

Maximum amount of results returned. Defaults to 100. Maximum value 100.

### MyAnimeList.API.DTOs.Requests.GetAnimeListRequest.Offset

The amount of results to skip. Defaults to 0.

### MyAnimeList.API.DTOs.Requests.GetAnimeListRequest.Search

The string to search with. Must be 3 characters or longer.

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Fields

An array containing the fields to be returned. Leave empty, for default fields.

#### Remarks

To use this property you must create a class that can map the new properties and use it in //TODO: Custom method.

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Limit

Maximum amount of results returned. Defaults to 100. Maximum value 100.

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Offset

The amount of results to skip. Defaults to 0.

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Season

Gets or sets the desired season

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Sort

Gets or sets the sorting field. Defaults to <a href="#myanimelist.api.sortby.anime_score">MyAnimeList.API.SortBy.ANIME_SCORE</a>

### MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest.Year

Gets or sets the year, defaults to the current year


## MALApiClient

Simple client to interact with MAL's API, requests are asyncronous.

### Constructor(Params)

Creates a new instance of the API Client.

| Name | Description |
| ---- | ----------- |
| Params | *MyAnimeList.API.UserParams*<br>s asd asd asd |

### GetAnimeList(MyAnimeList.API.DTOs.Requests.GetAnimeListRequest)

Returns a list of animes matching the provided string.

#### Returns

A <a href="#dtos.animelistresult">DTOs.AnimeListResult</a> containing the list of Animes, with their Name, Id and picture URLs

### GetSeasonalAnimeList(MyAnimeList.API.DTOs.Requests.GetSeasonalAnimeListRequest)

Returns a list of animes aired in the specified season.

#### Returns

A <a href="#dtos.seasonalanimelistresult">DTOs.SeasonalAnimeListResult</a> containing the list of Animes, with their Name, Id and picture URLs

### IsAuthenticated

Gets a value indicating whether the client is currently authenticated,

### MyAnimeList.API.UserParams.ClientId

The ClientId obtained from MyAnimeList. To register a new App and obtain a ClientId, visit 

### MyAnimeList.API.UserParams.OAuth2State

An opaque value used by the client to maintain state between the request and callback.

### MyAnimeList.API.UserParams.RedirectURI

This is the URI the user will be redirected after allowing the API usage, must mutch one of the URLs you provided when applying for an API key. By default it points http://localhost. But it's recomended to change this to a web service that will process the URL.
