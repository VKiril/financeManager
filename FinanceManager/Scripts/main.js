$(function () {
    function ShowFlashMessage(message, cssClass)
    {
        $(".flash-message-text").html(message);

        $(".flash-message")
            .removeClass("success")
            .removeClass("error")
            .addClass(cssClass)
            .fadeIn(300);

        setInterval(function () {
            $(".flash-message").fadeOut(300);
        }, 2000);
    }

    $("#purchase-create-submittt").on("click", function (e) {
        e.preventDefault();

        var formData = {
            Place: $("#Place").val(),
            Amount: $("#Amount").val(),
            NumberOfProducts: $("#NumberOfProducts").val(),
            UOM: $("#UOM").val(),
            CreatedAt: $("#CreatedAt").val(),
            PurchaseCategory: $("#select-purchase-category").val(),
            PurchaseEventCategory: $("#select-purchase-event-category").val()
        };

        var isFormValid = true;
        //Object.keys(formData).forEach(e => console.log(`key=${e}  value=${formData[e]}`));
        isFormValid = isFormValid & Object.keys(formData).forEach(function (e) {
            let temp = "" + formData[e];
            console.log(String(temp).length > 0);
            return String(temp).length > 0;
        });

        $.ajax({
            url: PURCHASE_CREATE,
            type: "POST",
            dataType: "json",
            data: formData,
            success: function (e) {
                $("#add-purchase-btn").removeClass("disabled");
            }
        });
    });

    $("#purchase-categoty-submit").on("click", function (e) {
        e.preventDefault();

        $.ajax({
            url: PURCHASE_CATEGORY_CREATE,
            type: "POST",
            data: {
                "Name": $("#purchase-category-name").val(),
                "Description": $("#purchase-category-description").val()
            },
            success: function (data) {
                ShowFlashMessage("Purchase Category Added!", "success");
                $("#purchase-category-name").val("");
                $("#purchase-category-description").val("");
                $("#purchase-categories").modal("hide");
            }
        });
    });

    $("#purchase-event-categoty-submit").on("click", function (e) {
        e.preventDefault();

        $.ajax({
            url: PURCHASE_EVENT_CATEGORY_CREATE,
            type: "POST",
            data: {
                "Name": $("#purchase-event-category-name").val(),
                "Description": $("#purchase-event-category-description").val(),
                "StartingDate": $("#purchase-event-starting-date").val()
            },
            success: function (data) {
                ShowFlashMessage("Purchase Event Category Added!", "success");
                $("#purchase-event-category-name").val("");
                $("#purchase-event-category-description").val("");
                $("#purchase-event-starting-date").val("");
                $("#purchase-event-categories").modal("hide");
            }
        });
    });

    $("#product-create-btn").on("click", function (e) {
        e.preventDefault();

        var formData = {
            Name: $("#Name").val(),
            Quantity: $("#Quantity").val(),
            ProductType: $("#ProductType").val(),
            Cost: $("#Cost").val(),
            CostPerUnit: $("#CostPerUnit").val(),
            //File: $("#product-image-tag").get(0).files,
            PurchaseIdReceiver: $("#add-purchase-btn").data("id"),
            ForWho: $("#ForWho").val(),
            IsMinimalNecesarry: $("#IsMinimalNecesarry").val()
        };

        //var form = new FormData($("product-create"));
        //var request = new XMLHttpRequest();
        //request.open("POST", PRODUCT_CREATE);
        //formData.append("product-image", $("#product-image-tag").files[0]);
        //request.send(formData);


        var isFormValid = true;
        //Object.keys(formData).forEach(e => console.log(`key=${e}  value=${formData[e]}`));
        isFormValid = isFormValid & Object.keys(formData).forEach(function (e) {
            let temp = "" + formData[e];
            console.log(String(temp).length > 0);
            return String(temp).length > 0;
        });

        $.ajax({
            url: PRODUCT_CREATE,
            type: "POST",
            dataType: "json",
            data: formData,
            error: function (e) {
                console.log(e);
            },
            success: function (e) {
                //$("#add-purchase-btn").removeClass("disabled");
                $("#no-products").html("");
                console.log(e);
                var img = "";
                if (e.FileName !== null) {
                    img = '<img style="width: 40px; height: 40px; margin: -5px 0px -5px 50px;" src="~/Uploads/' + e.FileName + '" alt="" />';
                }

                var html = '<tr> ' +
                                '<td>' + e.Name + '</td>' +
                                '<td>' + e.Quantity + '</td>' +
                                '<td>' + e.ProductType + '</td>' +
                                '<td>' + e.Cost + '</td>' +
                                '<td>' + e.CostPerUnit + '</td>' +
                                '<td>' + e.ForWho + '</td>' +
                                '<td>' + e.IsMinimalNecesarry  + '</td>' +
                                '<td>' + img + '</td>' +
                                '<td> ' +
                                '   <a href="/Products/Edit/' + e.ID + '">Edit</a> | ' +
                                '   <a href="/Products/Details/' + e.ID + '">Details</a> | ' +
                                '   <a href="/Products/Delete/' + e.ID + '">Delete</a> ' +
                                '</td> ' +
                    '</tr >';

                $("#product-modal").modal("hide");
                $("#purchase-product-list").removeClass("hide");
                $("#purchase-product-list").append(html);

                $("#Name").val("");
                $("#Quantity").val("");
                $("#ProductType").val("");
                $("#Cost").val("");
                $("#CostPerUnit").val("");
                $("#ForWho").val("");
                $("#IsMinimalNecesarry").val("");
            }
        });
    });
});
