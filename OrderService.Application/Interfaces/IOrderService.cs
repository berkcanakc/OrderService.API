﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task CheckoutAsync(CheckoutRequest request);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
    }
}
