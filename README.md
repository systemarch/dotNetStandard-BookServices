# Book Services

This repository is a .NET Standard 2.0 library that provides methods
for working with the book services APIs, such as Google Books.

Note that this library hasn't been published as a NuGet package yet,
and requires the NewtonSoft.Json (12.0.3) package to be manually added to the
project which uses it.

## Google Books API

See [Google Books API Getting Started](
    https://developers.google.com/books/docs/v1/getting_started) for reference.

### Currently implemented features

- Common Service class, with a method, used to
    send a GET request to the Books API with the given query
- Volume data classes, corresponding to the API response format
- VolumeService class with the methods, used to:
    - Perform a search for the given maximum number of volumes. Note that the search result depends on your IP address country (that includes availability and prices, which are in local currency)
        - with the given query
        - by the given author's name
    - Get the list of categories and publishers (which are hard-coded, because the API
    doesn't have the corresponding methods)

### Known issues

- The search method always sends requests to get 40 volumes
(which is the maximum number you can get with one request), even
if the provided maximum number is not divisible by 40

### Possible future improvements

- Add more search methods (f. e. by title)