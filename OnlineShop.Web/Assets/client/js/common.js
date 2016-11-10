var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        //$("#txtKeyword").autocomplete({
        //    minLength: 0,
        //    source: function (request, response) {
        //        $.ajax({
        //            url: "/Product/GetListProductByName",
        //            dataType: "json",
        //            data: {
        //                keyword: request.term
        //            },
        //            success: function (res) {
        //                response(res.data);
        //            }
        //        });
        //    },
        //    focus: function (event, ui) {
        //        $("#txtKeyword").val(ui.item.label);
        //        return false;
        //    },
        //    select: function (event, ui) {
        //        $("#txtKeyword").val(ui.item.label);
        //        return false;
        //    }
        //}).autocomplete("instance")._renderItem = function (ul, item) {
        //    return $("<li>")
        //      .append("<a>" + item.label + "</a>")
        //      .appendTo(ul);
        //};
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: productId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert('Thêm sản phẩm thành công.');
                        $.ajax({
                            url: '/ShoppingCart/GetAll',
                            type: 'GET',
                            dataType: 'json',
                            success: function (res) {
                                if (res.status) {
                                    var data = res.data;
                                    var total = 0;
                                    $.each(data, function (i, item) {
                                        total += (item.Product.Price * item.Quantity);
                                    });
                                    $('.simpleCart_total').html(numeral(total).format('0,0'));
                                    $('#simpleCart_quantity').text(numeral(data.length).format('0,0'));
                                }
                            }
                        });
                    }
                }
            });
        });
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });


    }
}
common.init();