﻿using System;
using System.Collections.Generic;

namespace VideoGameStore.Models;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual ShoppingCart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
