# QPager
QPager is asimple Library for ASP.NET Core Projects.

## Install Package with nuget
``` 
Install-Package QPager 
```

## How to use it ?

1. Add namespace to your Page
```
@using QPager
```
2.Call ``` PagedList ``` helper like below 

```
@Html.PagedList(currentPage, totalPage, page => Url.Action("Index", new { page }))
```
> ```currentPage``` and  ``` totalPage``` are some variables or viewbags 

## Customize Pager's Text
if you want to change default 'Next' and 'Prev' title you can doing this by passing another param like below  
```
@Html.PagedList(currentPage, totalPage, page => Url.Action("Index", new { page }),new QPageOptions { NextPageTitle="Next Page",PrevPageTitle="Prev Page"})
```

## Note :
1. Qpager uses bootstrap css , this mean you have to add bootstrap.css to all pages that you want to have pagination .if you don't like to use bootstrap you could 
add some css to decorate your pagination.

2.you have to update you `CurrentPage` int your action Like below :
```
 public IActionResult Index(int page=0)
        {
            ViewBag.currentPage = page++;
            return View();
        }
```
