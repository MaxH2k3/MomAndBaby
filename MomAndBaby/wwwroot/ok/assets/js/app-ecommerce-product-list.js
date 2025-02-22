"use strict";
$(function () {
  let e, s, a;
  a = (
    isDarkStyle
      ? ((e = config.colors_dark.borderColor),
        (s = config.colors_dark.bodyBg),
        config.colors_dark)
      : ((e = config.colors.borderColor),
        (s = config.colors.bodyBg),
        config.colors)
  ).headingColor;
  var t,
    n = $(".datatables-products"),
    o = {
      1: { title: "Scheduled", class: "bg-label-warning" },
      2: { title: "Publish", class: "bg-label-success" },
      3: { title: "Inactive", class: "bg-label-danger" },
    },
    c = {
      0: { title: "Household" },
      1: { title: "Office" },
      2: { title: "Electronics" },
      3: { title: "Shoes" },
      4: { title: "Accessories" },
      5: { title: "Game" },
    },
    i = { 0: { title: "Out_of_Stock" }, 1: { title: "In_Stock" } },
    r = { 0: { title: "Out of Stock" }, 1: { title: "In Stock" } };
  n.length &&
    ((t = n.DataTable({
      ajax: "/api/v1/products",
      columns: [
        { data: "id" },
        { data: "id" },
        { data: "name" },
        { data: "original" },
        { data: "stock" },
        { data: "company" },
        { data: "price" },
        { data: "quantity" },
        { data: "status" },
        { data: "" },
      ],
      columnDefs: [
        {
          className: "control",
          searchable: !1,
          orderable: !1,
          responsivePriority: 2,
          targets: 0,
          render: function (t, e, s, a) {
            return "";
          },
        },
        {
          targets: 1,
          orderable: !1,
          checkboxes: {
            selectAllRender: '<input type="checkbox" class="form-check-input">',
          },
          render: function () {
            return '<input type="checkbox" class="dt-checkboxes form-check-input" >';
          },
          searchable: !1,
        },
        {
          targets: 2,
          responsivePriority: 1,
          render: function (t, e, s, a) {
            var n = s.product_name,
              o = s.id,
              c = s.product_brand,
              i = s.image;
            return (
              '<div class="d-flex justify-content-start align-items-center product-name"><div class="avatar-wrapper"><div class="avatar avatar me-2 rounded-2 bg-label-secondary">' +
              (i
                ? '<img src="' +
                  assetsPath +
                  "img/ecommerce-images/" +
                  i +
                  '" alt="Product-' +
                  o +
                  '" class="rounded-2">'
                : '<span class="avatar-initial rounded-2 bg-label-' +
                  [
                    "success",
                    "danger",
                    "warning",
                    "info",
                    "dark",
                    "primary",
                    "secondary",
                  ][Math.floor(6 * Math.random())] +
                  '">' +
                  (i = (
                    ((i = (n = s.product_brand).match(/\b\w/g) || []).shift() ||
                      "") + (i.pop() || "")
                  ).toUpperCase()) +
                  "</span>") +
              '</div></div><div class="d-flex flex-column"><h6 class="text-body text-nowrap mb-0">' +
              n +
              '</h6><small class="text-muted text-truncate d-none d-sm-block">' +
              c +
              "</small></div></div>"
            );
          },
        },
        {
          targets: 3,
          responsivePriority: 5,
          render: function (t, e, s, a) {
            s = c[s.category].title;
            return (
              "<span class='text-truncate d-flex align-items-center'>" +
              {
                Household:
                  '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-warning me-2"><i class="bx bx-home-alt"></i></span>',
                Office:
                  '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-success me-2"><i class="bx bx-briefcase"></i></span>',
                Electronics:
                  '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-primary me-2"><i class="bx bx-mobile-alt"></i></span>',
                Shoes:
                  '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-info me-2"><i class="bx bx-walk"></i></span>',
                Accessories:
                  '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-secondary me-2"><i class="bx bxs-watch"></i></span>',
                Game: '<span class="avatar-sm rounded-circle d-flex justify-content-center align-items-center bg-label-dark me-2"><i class="bx bx-game"></i></span>',
              }[s] +
              s +
              "</span>"
            );
          },
        },
        {
          targets: 4,
          orderable: !1,
          responsivePriority: 3,
          render: function (t, e, s, a) {
            s = s.stock;
            return (
              "<span class='text-truncate'>" +
              {
                Out_of_Stock:
                  '<label class="switch switch-primary switch-sm"><input type="checkbox" class="switch-input" id="switch"><span class="switch-toggle-slider"><span class="switch-off"></span></span></label>',
                In_Stock:
                  '<label class="switch switch-primary switch-sm"><input type="checkbox" class="switch-input" checked=""><span class="switch-toggle-slider"><span class="switch-on"></span></span></label>',
              }[i[s].title] +
              '<span class="d-none">' +
              i[s].title +
              "</span></span>"
            );
          },
        },
        {
          targets: 5,
          render: function (t, e, s, a) {
            return "<span>" + s.sku + "</span>";
          },
        },
        {
          targets: 6,
          render: function (t, e, s, a) {
            return "<span>" + s.price + "</span>";
          },
        },
        {
          targets: 7,
          responsivePriority: 4,
          render: function (t, e, s, a) {
            return "<span>" + s.qty + "</span>";
          },
        },
        {
          targets: -2,
          render: function (t, e, s, a) {
            s = s.status;
            return (
              '<span class="badge ' +
              o[s].class +
              '" text-capitalized>' +
              o[s].title +
              "</span>"
            );
          },
        },
        {
          targets: -1,
          title: "Actions",
          searchable: !1,
          orderable: !1,
          render: function (t, e, s, a) {
            return '<div class="d-inline-block text-nowrap"><button class="btn btn-sm btn-icon"><i class="bx bx-edit"></i></button><button class="btn btn-sm btn-icon dropdown-toggle hide-arrow" data-bs-toggle="dropdown"><i class="bx bx-dots-vertical-rounded me-2"></i></button><div class="dropdown-menu dropdown-menu-end m-0"><a href="javascript:0;" class="dropdown-item">View</a><a href="javascript:0;" class="dropdown-item">Suspend</a></div></div>';
          },
        },
      ],
      order: [2, "asc"],
      dom: '<"card-header d-flex border-top rounded-0 flex-wrap py-md-0"<"me-5 ms-n2 pe-5"f><"d-flex justify-content-start justify-content-md-end align-items-baseline"<"dt-action-buttons d-flex align-items-start align-items-md-center justify-content-sm-center mb-3 mb-sm-0"lB>>>t<"row mx-2"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
      lengthMenu: [7, 10, 20, 50, 70, 100],
      language: {
        sLengthMenu: "_MENU_",
        search: "",
        searchPlaceholder: "Search Product",
        info: "Displaying _START_ to _END_ of _TOTAL_ entries",
      },
      buttons: [
        {
          extend: "collection",
          className: "btn btn-label-secondary dropdown-toggle me-3",
          text: '<i class="bx bx-export me-1"></i>Export',
          buttons: [
            {
              extend: "print",
              text: '<i class="bx bx-printer me-2" ></i>Print',
              className: "dropdown-item",
              exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7],
                format: {
                  body: function (t, e, s) {
                    var a;
                    return t.length <= 0
                      ? t
                      : ((t = $.parseHTML(t)),
                        (a = ""),
                        $.each(t, function (t, e) {
                          void 0 !== e.classList &&
                          e.classList.contains("product-name")
                            ? (a += e.lastChild.firstChild.textContent)
                            : void 0 === e.innerText
                            ? (a += e.textContent)
                            : (a += e.innerText);
                        }),
                        a);
                  },
                },
              },
              customize: function (t) {
                $(t.document.body)
                  .css("color", a)
                  .css("border-color", e)
                  .css("background-color", s),
                  $(t.document.body)
                    .find("table")
                    .addClass("compact")
                    .css("color", "inherit")
                    .css("border-color", "inherit")
                    .css("background-color", "inherit");
              },
            },
            {
              extend: "csv",
              text: '<i class="bx bx-file me-2" ></i>Csv',
              className: "dropdown-item",
              exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7],
                format: {
                  body: function (t, e, s) {
                    var a;
                    return t.length <= 0
                      ? t
                      : ((t = $.parseHTML(t)),
                        (a = ""),
                        $.each(t, function (t, e) {
                          void 0 !== e.classList &&
                          e.classList.contains("product-name")
                            ? (a += e.lastChild.firstChild.textContent)
                            : void 0 === e.innerText
                            ? (a += e.textContent)
                            : (a += e.innerText);
                        }),
                        a);
                  },
                },
              },
            },
            {
              extend: "excel",
              text: '<i class="bx bxs-file-export me-2"></i>Excel',
              className: "dropdown-item",
              exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7],
                format: {
                  body: function (t, e, s) {
                    var a;
                    return t.length <= 0
                      ? t
                      : ((t = $.parseHTML(t)),
                        (a = ""),
                        $.each(t, function (t, e) {
                          void 0 !== e.classList &&
                          e.classList.contains("product-name")
                            ? (a += e.lastChild.firstChild.textContent)
                            : void 0 === e.innerText
                            ? (a += e.textContent)
                            : (a += e.innerText);
                        }),
                        a);
                  },
                },
              },
            },
            {
              extend: "pdf",
              text: '<i class="bx bxs-file-pdf me-2"></i>Pdf',
              className: "dropdown-item",
              exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7],
                format: {
                  body: function (t, e, s) {
                    var a;
                    return t.length <= 0
                      ? t
                      : ((t = $.parseHTML(t)),
                        (a = ""),
                        $.each(t, function (t, e) {
                          void 0 !== e.classList &&
                          e.classList.contains("product-name")
                            ? (a += e.lastChild.firstChild.textContent)
                            : void 0 === e.innerText
                            ? (a += e.textContent)
                            : (a += e.innerText);
                        }),
                        a);
                  },
                },
              },
            },
            {
              extend: "copy",
              text: '<i class="bx bx-copy me-2" ></i>Copy',
              className: "dropdown-item",
              exportOptions: {
                columns: [1, 2, 3, 4, 5, 6, 7],
                format: {
                  body: function (t, e, s) {
                    var a;
                    return t.length <= 0
                      ? t
                      : ((t = $.parseHTML(t)),
                        (a = ""),
                        $.each(t, function (t, e) {
                          void 0 !== e.classList &&
                          e.classList.contains("product-name")
                            ? (a += e.lastChild.firstChild.textContent)
                            : void 0 === e.innerText
                            ? (a += e.textContent)
                            : (a += e.innerText);
                        }),
                        a);
                  },
                },
              },
            },
          ],
        },
        {
          text: '<i class="bx bx-plus me-0 me-sm-1"></i><span class="d-none d-sm-inline-block">Add Product</span>',
          className: "add-new btn btn-primary",
          action: function () {
            window.location.href = "app-ecommerce-product-add.html";
          },
        },
      ],
      responsive: {
        details: {
          display: $.fn.dataTable.Responsive.display.modal({
            header: function (t) {
              return "Details of " + t.data().product_name;
            },
          }),
          type: "column",
          renderer: function (t, e, s) {
            s = $.map(s, function (t, e) {
              return "" !== t.title
                ? '<tr data-dt-row="' +
                    t.rowIndex +
                    '" data-dt-column="' +
                    t.columnIndex +
                    '"><td>' +
                    t.title +
                    ":</td> <td>" +
                    t.data +
                    "</td></tr>"
                : "";
            }).join("");
            return !!s && $('<table class="table"/><tbody />').append(s);
          },
        },
      },
      initComplete: function () {
        this.api()
          .columns(-2)
          .every(function () {
            var e = this,
              s = $(
                '<select id="ProductStatus" class="form-select text-capitalize"><option value="">Status</option></select>'
              )
                .appendTo(".product_status")
                .on("change", function () {
                  var t = $.fn.dataTable.util.escapeRegex($(this).val());
                  e.search(t ? "^" + t + "$" : "", !0, !1).draw();
                });
            e.data()
              .unique()
              .sort()
              .each(function (t, e) {
                s.append(
                  '<option value="' +
                    o[t].title +
                    '">' +
                    o[t].title +
                    "</option>"
                );
              });
          }),
          this.api()
            .columns(3)
            .every(function () {
              var e = this,
                s = $(
                  '<select id="ProductCategory" class="form-select text-capitalize"><option value="">Category</option></select>'
                )
                  .appendTo(".product_category")
                  .on("change", function () {
                    var t = $.fn.dataTable.util.escapeRegex($(this).val());
                    e.search(t ? "^" + t + "$" : "", !0, !1).draw();
                  });
              e.data()
                .unique()
                .sort()
                .each(function (t, e) {
                  s.append(
                    '<option value="' +
                      c[t].title +
                      '">' +
                      c[t].title +
                      "</option>"
                  );
                });
            }),
          this.api()
            .columns(4)
            .every(function () {
              var e = this,
                s = $(
                  '<select id="ProductStock" class="form-select text-capitalize"><option value=""> Stock </option></select>'
                )
                  .appendTo(".product_stock")
                  .on("change", function () {
                    var t = $.fn.dataTable.util.escapeRegex($(this).val());
                    e.search(t ? "^" + t + "$" : "", !0, !1).draw();
                  });
              e.data()
                .unique()
                .sort()
                .each(function (t, e) {
                  s.append(
                    '<option value="' +
                      i[t].title +
                      '">' +
                      r[t].title +
                      "</option>"
                  );
                });
            });
      },
    })),
    $(".dataTables_length").addClass("mt-0 mt-md-3 me-3"),
    $(".dt-buttons > .btn-group > button").removeClass("btn-secondary"),
    $(".dt-buttons").addClass("d-flex flex-wrap")),
    $(".datatables-products tbody").on("click", ".delete-record", function () {
      t.row($(this).parents("tr")).remove().draw();
    }),
    setTimeout(() => {
      $(".dataTables_filter .form-control").removeClass("form-control-sm"),
        $(".dataTables_length .form-select").removeClass("form-select-sm");
    }, 300);
});
