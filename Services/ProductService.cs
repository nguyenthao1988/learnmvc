using System.Collections.Generic;
using App.Models;

namespace App.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService(){
            this.AddRange(new ProductModel[] {
                new ProductModel() {Id = 1, Name = "Iphone X", Price = 1000},
                new ProductModel() {Id = 2, Name = "Samsung note 21", Price = 500},
                new ProductModel() {Id = 3, Name = "Redmi Note 11", Price = 800},
                new ProductModel() {Id = 4, Name = "Oppo Find X3", Price = 300},
            });
        }
    }
}