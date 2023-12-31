﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Shared;

[Index("CategoryId", Name = "CategoriesProducts")]
[Index("CategoryId", Name = "CategoryId")]
[Index("ProductName", Name = "ProductName")]
[Index("SupplierId", Name = "SupplierId")]
[Index("SupplierId", Name = "SuppliersProducts")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(40)]
    public string ProductName { get; set; } = null!;

    public int? SupplierId { get; set; }
    
    public int? CategoryId { get; set; }

    [StringLength(20)]
    public string? QuantityPerUnit { get; set; }

    [Column(TypeName = "money")]
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    [XmlIgnore]
    [JsonIgnore]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    [XmlIgnore]
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Products")]
    [XmlIgnore]
    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }
}
