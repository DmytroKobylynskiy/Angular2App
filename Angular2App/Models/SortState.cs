using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class SortState
    {
        public enum SortingState
        {
            StartPointAsc,    // по имени по возрастанию
            StartPointDesc,   // по имени по убыванию
            EndPointAsc,    // по имени по возрастанию
            EndPointDesc,   // по имени по убыванию
            DateAsc, // по возрасту по возрастанию
            DateDesc,    // по возрасту по убыванию
            StatusOrderAsc, // по компании по возрастанию
            StatusOrderDesc // по компании по убыванию
        }
    }
}
