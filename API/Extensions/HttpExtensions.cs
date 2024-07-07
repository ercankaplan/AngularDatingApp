
namespace API.Extensions;

using System.Text.Json;
using API.Helpers;

public static class HttpExtensions
{

    public static void AddPaginationHeader(this HttpResponse responser, PaginationHeader paginationHeader)
    {

        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase

        };
        responser.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
        responser.Headers.Append("Access-Control-Expose-Headers", "Pagination");


    }
}