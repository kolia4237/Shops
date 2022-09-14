using Shop.Domain.Enum;

namespace Shop.Domain.Entity
{
    public class Cloth
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public byte[] Avatar { get; set; }
        
        public Gender Gender { get; set; }
    }
}