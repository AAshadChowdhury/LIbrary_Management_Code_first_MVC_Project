/// <reference path="jquery-3.4.1.min.js" />

$(document).ready(function () {
    getAllCategories();
    function getAllCategories() {
        $.getJSON("/api/categoriesTBsAPI",
            function (data) { BindtoCategoriesTable(data); });
    }
    function BindtoCategoriesTable(data) {
        $("#CategoriesTable tbody").empty();
        for (var i = 0; i < data.length; i++) {
            var record = data[i];
            $("#CategoriesTable tbody").append(`<tr onclick="BindCategoryFormWithBooksTable('${record.category}','${record.categoryInfo}')"><td>${record.category}</td><td>${record.categoryInfo}</td></tr>`);
        };
    }
    function BindToBooksTable(data) {
        $("#BooksTable tbody").empty();
        for (var i = 0; i < data.length; i++) {
            var book = data[i];
            $("#BooksTable tbody").append(`<tr onclick="loadBookForEdit('${book.id}','${book.name}','${book.author}','${book.publisher}','${book.categoriesTBcategory}','${book.stockQuantity}','${book.price}','${book.cover}')"><td>${book.id}</td><td>${book.name}</td><td>${book.author}</td><td>${book.publisher}</td><td>${book.categoriesTBcategory}</td><td>${book.stockQuantity}</td><td>${book.price}</td><td><img width='100' src='/coverimage/${book.cover}' alt='no image'/></td></tr>`);
        };
    }
    $("#add").click(function () {
        $.ajax('/api/categoriesTBsAPI',
            {
                data: { category: $("#category").val(), categoryInfo: $("#categoryInfo").val() },
                dataType: 'json',
                method: 'post',
                timeout: 500,
                success: function (data, status, xhr) {
                    $('#out').html("Successfuly added");
                    getAllCategories();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#out').html('Error: ' + errorMessage);
                }
            });
    });
    $("#update").click(function () {
        $.ajax('/api/categoriesTBsAPI/' + $("#category").val(),
            {
                data: { category: $("#category").val(), categoryInfo: $("#categoryInfo").val() },
                dataType: 'json',
                method: 'put',
                timeout: 500,
                success: function (data, status, xhr) {
                    $('#out').html("Successfuly updated");
                    getAllCategories();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#out').html('Error: ' + errorMessage);
                }
            });
    });
    $("#delete").click(function () {
        $.ajax('/api/categoriesTBsAPI/' + $("#category").val(),
            {
                dataType: 'json',
                method: 'delete',
                timeout: 500,
                success: function (data, status, xhr) {
                    $('#out').html("Successfuly delete");
                    getAllCategories();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#out').html('Error: ' + errorMessage);
                }
            });
    });
    BindCategoryFormWithBooksTable = function (category, categoryInfo) {
        $("#category").val(category);
        $("#categoryInfo").val(categoryInfo);
        alert("/api/booksAPI?categoriesTBcategory=" + category);
        $.getJSON("/api/booksAPI?categoriesTBcategory=" + category,
            function (data) { BindToBooksTable(data); });
    }


    //Codes for Books Table----------------------------------------------------------------------------------------------------------------------

    function getAllBooks() {
        $.getJSON("/api/booksAPI",
            function (data) { BindToBooksTable(data); });
    }


    $("#addBook").click(function () {
        $.ajax('/api/booksAPI',
            {
                data: { id: $("#bookid").val(), name: $("#name").val(), author: $("#author").val(), category: $("#categoriesTBcategory").val(), publisher: $("#publisher").val(), stockQuantity: $("#stockQuantity").val(), price: $("#price").val(), cover: $('#ImageFile').val().split('\\').pop() },
                dataType: 'json',
                method: 'post',
                timeout: 500,
                success: function (data, status, xhr) {
                    UploadData();
                    $('#iout').html("Successfuly added");
                    getAllBooks();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#iout').html('Error: ' + errorMessage);
                }
            });
    });
    $("#updateBook").click(function () {
        $.ajax('/api/booksAPI/' + $("#id").val(),
            {
                data: { id: $("#id").val(), name: $("#name").val(), author: $("#author").val(), category: $("#categoriesTBcategory").val(), publisher: $("#publisher").val(), stockQuantity: $("#stockQuantity").val(), price: $("#price").val(), cover: $('#cover').val() },
                dataType: 'json',
                method: 'put',
                timeout: 500,
                success: function (data, status, xhr) {
                    $('#out').html("Successfuly updated");
                    if ($('#ImageFile').val().split('\\').pop() != "")
                        UploadData();
                    getAllBooks();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#iout').html('Error: ' + errorMessage);
                }
            });
    });
    $("#deleteBook").click(function () {
        $.ajax('/api/booksAPI/' + $("#id").val(),
            {
                dataType: 'json',
                method: 'delete',
                timeout: 500,
                success: function (data, status, xhr) {
                    $('#iout').html("Successfuly delete");
                    getAllBooks();
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    $('#iout').html('Error: ' + errorMessage);
                }
            });
    });
    UploadData = function () {
        if (window.FormData !== undefined) {
            var fileUpload = $("#ImageFile").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            fileData.append('username', 'Manas');
            $.ajax({
                url: '/AjaxMultipleCrud/UploadFiles',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    alert(result);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    }
    $("#ImageFile").change(function () {
        $("#cover").val($('#ImageFile').val().split('\\').pop());
    })
});
function loadBookForEdit(id, name, author, publisher, category, stockQuantity, price, cover) {
    $("#id").val(id);
    $("#name").val(name);
    $("#author").val(author);
    $("#publisher").val(publisher);
    $("#categoryb").val(category);
    $("#stockQuantity").val(stockQuantity);
    $("#price").val(price);
    $("#cover").val(cover);
}



//});
