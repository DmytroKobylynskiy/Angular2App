interface TaxiOrder {
    taxiOrderId : string;
    startPoint : string;
    endPoint : string;
    date : string;
    time : string;
    withAnimals : boolean;
    freightCar : boolean;
    distanse : number;
    duration : number;
    orderOwnerId : string;
    orderStatus : string;
    expectedDate : string;
    expectedTime : string;
    receiverId : string;
    condition : boolean;   
    conditionAgree : boolean;  
}