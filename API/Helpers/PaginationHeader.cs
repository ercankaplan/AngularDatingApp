// Purpose: This file is used to create a class that will be used to create a pagination header for the response of the API. 
//This class will be used to create the pagination header that will be sent back to the client. 
//The pagination header will contain the current page, items per page, total items, and total pages. 
namespace API.Helpers;
public class PaginationHeader 
{
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    
}