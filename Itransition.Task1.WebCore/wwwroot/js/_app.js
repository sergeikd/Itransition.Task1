function AppViewModel() {
    var self = this;

    self.appData = {
        amount: ko.observable(),
        put: ko.observable(),
        transfer: ko.observable(),
        othersAccounts: ko.observableArray(),
        selectedAccount: ko.observable(),
        transactions: ko.observableArray(),
        pageCount: ko.observable(0),
        currentPage: ko.observable(1),
        pageSize: ko.observable(10)   
    };

    var sortBy = "Id";
    self.sorting = function(item, event) {
        sortBy = $(event.target).html();
        self.loadData();
    };

    self.appData.pageSizeChanged = function (obj, event) {
        if (event.originalEvent) { //user made a change
            self.loadData();
        }
    };


    function transaction(id, date, sender, amount, recipient) {
        this.Id = id;
        this.Date = date;
        this.Sender = sender;
		this.Amount = amount;
		this.Recipient = recipient;
    }

    self.loadData = function () {
        var dataToServer = {
            pageSize: self.appData.pageSize(),
            currentPage: self.appData.currentPage(),
            sort: sortBy
        };
        $.ajax({
            url: "/Home/GetData",
            type: "POST",
            data: dataToServer,
            contentType: 'application/x-www-form-urlencoded'
        }).success(self.successHandler).error(self.errorHandler);
    };

    self.putMoney = function (putValue) {
        var data = {
            put: putValue
        };

        $.ajax({
            url: "/Home/Put",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded'
        })
            .success(function (response) {
                if (response) {
                    $("#ActionFailText").after(response);
                    $("#ActionFailModal").modal("show");
                }
                else {
                    $("#ajaxOkModal").modal("show");
                    self.loadData();
                }
            })
            .error(self.errorHandler);
    };
    self.transferMoney = function (transferValue) {
        var data = {
            transfer: transferValue,
            recipient: self.appData.selectedAccount()
        };

        $.ajax({
                url: "/Home/Transfer",
                type: "POST",
                data: data,
                contentType: "application/x-www-form-urlencoded"
            })
            .success(function (response) {
                if (response) {
                    $("#ActionFailText").text("").append(response);
                    $("#ActionFailModal").modal("show");
                }
                else {
                    $("#ajaxOkModal").modal("show");
                    self.loadData();
                }
            })
            .error(self.errorHandler);
    };

    self.successHandler = function (data) {
        self.appData.amount(data.amount);
        self.appData.othersAccounts(data.othersAccounts);
        self.appData.put("");
        self.appData.transfer("");
        self.appData.selectedAccount("");
        //self.appData.pageCount(data.Transactions.PageCount);
        //self.appData.currentPage(data.Transactions.CurrentPage);
        //self.appData.pageSize(data.Transactions.PageSize);
        //var arrTranscts = data.Transactions.Results.map(function (item) {
        //    item.Date = new Date(parseFloat(item.Date.replace("/Date(", "").replace(")/", ""))); //change Date format from /Date(012345679)/ to normal
        //    return item;
        //});
        //self.appData.transactions(arrTranscts);

    };
    self.errorHandler = function () {
        $("#ajaxErrorModal").modal("show");
    };
}
var appViewModel = new AppViewModel();
ko.applyBindings(appViewModel);

appViewModel.loadData();

//*** PUT MONEY action handler ***
var putBtn = document.getElementById("putBtn");
putBtn.addEventListener("click", function (e) {
    var putValue = appViewModel.appData.put();
    if (putValue === undefined || putValue === "") {
        $("#ActionFailText").text("").append("The input field has an incorrect value");
        $("#ActionFailModal").modal("show");
        return;
    }
    appViewModel.putMoney(putValue);
});
//*** End PUT MONEY action handler ***

//*** TRANSFER MONEY action handler ***
var putBtn = document.getElementById("transferBtn");
putBtn.addEventListener("click", function (e) {
    var selectedAccount = appViewModel.appData.selectedAccount();
    if (selectedAccount === undefined || !selectedAccount) {
        $("#ActionFailText").text("").append("An account wasn't choosen in the drop-down list");
        $("#ActionFailModal").modal("show");
        return;
    }
    var transferValue = appViewModel.appData.transfer();
    if (transferValue === undefined || transferValue === "") {
        $("#ActionFailText").text("").append("The input field has an incorrect value");
        $("#ActionFailModal").modal("show");
        return;
    }
    appViewModel.transferMoney(transferValue);
});
//*** End TRANSFER MONEY action handler ***