using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        
        public int ClothId { get; set; }
        
        public int UserId { get; set; }
    }
}