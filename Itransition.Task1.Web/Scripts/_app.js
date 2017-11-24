function AppViewModel() {
    var self = this;
    
    self.appData = {
        amount: ko.observable(),
        put: ko.observable(""),
        transfer: ko.observable(""),
        othersAccounts: ko.observableArray(),
        selectedAccount: ko.observable(),
        transactions: ko.observableArray()
    };

    function transaction(id, date, sender, amount, recipient) {
        this.Id = id;
        this.Date = JSON.parse(date, function() {
            return new Date(date);
        });
	    this.Sender = sender;
		this.Amount = amount;
		this.Recipient = recipient;
	};
    self.loadInitData = function () {
        $.ajax({
            url: '/Home/GetGlobalData',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(self.successHandler).error(self.errorHandler);
    };

    self.putMoney = function (putValue) {
        var data = {
            put: putValue
        };

        $.ajax({
            url: '/Home/Put',
            type: 'POST',
            data: data,
            contentType: 'application/x-www-form-urlencoded'
        })
            .success(self.successHandler)
            .error(self.errorHandler);
    };
    self.transferMoney = function (transferValue) {
        var data = {
            transfer: transferValue,
            recipient: self.appData.selectedAccount()
        };

        $.ajax({
            url: '/Home/Transfer',
            type: 'POST',
            data: data,
            contentType: 'application/x-www-form-urlencoded'
        })
            .success(self.successHandler)
            .error(self.errorHandler);
    };

    self.successHandler = function (data) {
        self.appData.amount(data.UserAmount);
        self.appData.othersAccounts(data.OthersAccounts);
        self.appData.transactions(data.Transactions);
        var aaa = data.Transactions;
        var bbb = self.appData.transactions;
        self.appData.put("");
        self.appData.transfer("");
        self.appData.selectedAccount("");
    };
    self.errorHandler = function () {
        alert("Error!");
    };
};
var appViewModel = new AppViewModel();
ko.applyBindings(appViewModel);

appViewModel.loadInitData();

//*** PUT MONEY action handler ***
var putBtn = document.getElementById("putBtn");
putBtn.addEventListener("click", function (e) {
    var putValue = appViewModel.appData.put();
    if (putValue == undefined || putValue === "") {
        return;
    }
    appViewModel.putMoney(putValue);
});
//*** End PUT MONEY action handler ***

//*** TRANSFER MONEY action handler ***
var putBtn = document.getElementById("transferBtn");
putBtn.addEventListener("click", function (e) {
    var selectedAccount = appViewModel.appData.selectedAccount();
    if (selectedAccount == undefined || selectedAccount.length < 16) {
        return;
    }
    var transferValue = appViewModel.appData.transfer();
    if (transferValue == undefined || transferValue === "") {
        return;
    }
    appViewModel.transferMoney(transferValue);
});
//*** End TRANSFER MONEY action handler ***