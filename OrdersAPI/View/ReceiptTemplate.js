
//Helpers

function getOrderID() {
    const urlParams = new URLSearchParams(window.location.search);
    const orderID = urlParams.get('id');
    return orderID;
}

function createRow(classList, innerText) {
    let childNode = document.createElement('td');
    childNode.textContent = innerText;
    childNode.className = classList;
    return childNode;
}

function updateSuccessCallback(response) {
    alert(response["message"]);
}

function getReceiptDetailsCallback(response) {
    let address_arr = response.data["address"].split(',');
    let cardData = response.data['cardName'];
    let cardData_arr = [];
    if (cardData !== null) {
        cardData_arr = cardData.split(' ');
    }

    let tbody = document.createElement('tbody');
    response.data['ordersDetails'].forEach(order => {
        console.log(order);
        let tableRow = document.createElement("tr");
        tableRow.appendChild(createRow("first_column no-col", order['quantity']));
        tableRow.appendChild(createRow("first_column", order['name']));
        tableRow.appendChild(createRow("", `$${order['price']}`));
        tbody.appendChild(tableRow);
    });
    document.getElementsByClassName('tbl_main')[0].appendChild(tbody);

    $("#address_1").text(address_arr[0]);
    $("#address_2").text(address_arr.slice(1, address_arr.length - 1).join(","));
    $("#createdDate").text(response.data["createdDate"]);
    $("#createdDate_actual").text(response.data["createdDate"]);
    $("#companyName").text(response.data["companyName"]);
    $("#phoneNumber").text(response.data["phoneNumber"]);
    $("#url").text(response.data["url"]);
    $("#transaction").text(response.data["transaction"]);
    $("#type_Of_Sale").text(response.data["type_Of_Sale"]);
    $("#reference").text(response.data["reference"]);
    $("#auth_ID").text(response.data["auth_ID"]);
    $("#mid").text(response.data["mid"]);
    $("#aid").text(response.data["aid"]);
    $("#order").text(response.data["order"]);
    $("#payment").text(response.data["payment"]);
    $("#total").text(`$${response.data["total"]}`);
    $("#total_payment").text(`$${response.data["total"]}`);
    $("#Total_bill").text(`$${response.data["total"]}`);
    $("#auth_network_name").text(cardData_arr[0]);
    $("#sale_card").text(cardData_arr.join(' ').replaceAll('X', ''));
}

function updateCustomerDetails() {
    let isFormValid = $('#txt_email').val() !== '' && $('#txt_number').val() !== '';
    if (isFormValid) {

        let orderID = getOrderID();
        var settings = {
            "url": `https://coorder.azurewebsites.net/v1/api/order/${orderID}`,
            "method": "PUT",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "CustomerEmail": $('#txt_email').val(),
                "CustomerPhone": $('#txt_number').val()
            }),
            "success": updateSuccessCallback,
            "error": updateSuccessCallback
        };

        $.ajax(settings);
    }
}

function getReceiptDetails() {
    
    let orderID = getOrderID();
    var settings = {
        "url": `https://coorder.azurewebsites.net/v1/api/order/${orderID}`,
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "CustomerEmail": "ameerhamza0334@gmail.com",
            "CustomerPhone": "(123) 4567 890"
        }),
        "success": getReceiptDetailsCallback,
        "error": updateSuccessCallback
    };

    $.ajax(settings);
}
// Helpers End

$(document).ready(function () {
    getReceiptDetails();
    $("#btn_submit").click(updateCustomerDetails);
});