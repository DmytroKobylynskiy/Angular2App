interface NotificationOrder {
    notificationId : number;
    notificationStatus :string;
    receiverId : string;
    orderOwnerId : string;
    orderStatus : string;
    orderId : number;
    condition : boolean;
    conditionAgree : boolean;
    isLoad : boolean;
    taxiOrder : TaxiOrder;
    notificationTitle : string;
}