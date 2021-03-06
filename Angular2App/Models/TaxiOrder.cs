﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Angular2App.Models
{
    public class TaxiOrder
    {
        public int Id { get; set; }
       // [Required]
        //[RegularExpression(@"/^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$/u", ErrorMessage = "Недопустимая начальная точка")]
        public string StartPoint { get; set; }
        //[Required]
        //[RegularExpression(@"/^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$/u", ErrorMessage = "Недопустимая конечная точка")]
        public string EndPoint { get; set; }
        //[Required]
       // [Range(typeof(DateTime), "11/12/2016", "1/1/2020")]
        public string Date { get; set; }
      //  [Required]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
       // [DataType(DataType.Time)]
        public string Time { get; set; }
        public bool WithAnimals { get; set; }
        public bool FreightCar { get; set; }
        //[Required]
       // [Range(1,10000,ErrorMessage = "Расстояние от 1 до 10000")]
        public float Distanse { get; set; }
       // [Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
       // [DataType(DataType.Time)]
        public float Duration { get; set; }
       //[Required]
       // [Phone(ErrorMessage = "Недопустимый номер телефона")]
        public string OrderOwnerId { get; set; }
        public string OrderOwnerEmail { get; set; }
        public string OrderStatus { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverEmail{get;set;}
        
    }
}
