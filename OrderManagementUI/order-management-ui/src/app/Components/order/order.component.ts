import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Product } from '../../Models/product.Model';
import { Discount } from '../../Models/discount.model';
import { OrderDetail } from '../../Models/order-detail.model';
import { Order } from '../../Models/order.model';

import { OrderService } from '../../Services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  // Dropdown Data

  products: Product[] = [];
  discounts: Discount[] = [];

  // Selected Product / Discount

  selectedProductId: number = 0;
  quantity: number = 1;
  selectedDiscountId: number = 0;

  // Current Calculated Detail

  currentDetail: OrderDetail | null = null;

  // Order Details

  orderDetails: OrderDetail[] = [];

  // Order Information

  orderCode: string = '';

  orderDate: string = '';

  billingAddress: string = '';

  shippingAddress: string = '';

  remark: string = '';


  // Order Totals

  subTotal: number = 0;

  totalDiscount: number = 0;

  grandTotal: number = 0;


  // Messages

  successMessage: string = '';

  errorMessage: string = '';


  // Constructor

  constructor(
    private orderService: OrderService
  ) {}


  // On Init

  ngOnInit(): void {

    // Set today's date
    this.orderDate = new Date()
      .toISOString()
      .substring(0, 10);

    // Load dropdown data
    this.loadProducts();

    this.loadDiscounts();
  }


  // Load Products

  loadProducts(): void {

    this.orderService.getProducts().subscribe({

      next: (response: Product[]) => {

        console.log('Products:', response);

        this.products = response;
      },

      error: (error) => {

        console.error('Product API Error:', error);

        this.errorMessage =
          error.error?.message ??
          'Unable to load products.';
      }

    });
  }


  // Load Discounts

  loadDiscounts(): void {

    this.orderService.getDiscounts().subscribe({

      next: (response: Discount[]) => {

        console.log('Discounts:', response);

        this.discounts = response;
      },

      error: (error) => {

        console.error('Discount API Error:', error);

        this.errorMessage =
          error.error?.message ??
          'Unable to load discounts.';
      }

    });
  }


  // Calculate Detail

  calculateDetail(): void {

    this.clearMessages();

    // Product validation
    if (this.selectedProductId <= 0) {

      this.errorMessage =
        'Please select product.';

      return;
    }


    // Quantity validation
    if (this.quantity <= 0) {

      this.errorMessage =
        'Quantity must be greater than zero.';

      return;
    }


    // Discount validation
    if (this.selectedDiscountId <= 0) {

      this.errorMessage =
        'Please select discount.';

      return;
    }


    // API call
    this.orderService.calculateDetail(
      this.selectedProductId,
      this.quantity,
      this.selectedDiscountId
    ).subscribe({

      next: (response: OrderDetail) => {

        console.log(
          'Calculated Detail:',
          response
        );

        this.currentDetail = response;
      },

      error: (error) => {

        console.error(
          'Calculate Detail API Error:',
          error
        );

        this.errorMessage =
          error.error?.message ??
          'Unable to calculate order detail.';
      }

    });
  }


  // Add Detail

  addDetail(): void {

    this.clearMessages();


    // Check calculated detail
    if (!this.currentDetail) {

      this.errorMessage =
        'Please calculate the order detail first.';

      return;
    }


    // Add calculated detail
    this.orderDetails.push({
      ...this.currentDetail
    });


    // Recalculate totals
    this.calculateTotals();


    // Reset selection
    this.selectedProductId = 0;

    this.quantity = 1;

    this.selectedDiscountId = 0;

    this.currentDetail = null;
  }


  // Remove Detail

  removeDetail(index: number): void {

    this.orderDetails.splice(index, 1);

    this.calculateTotals();
  }


  // Calculate Totals

  calculateTotals(): void {

    this.subTotal =
      this.orderDetails.reduce(
        (total, detail) =>
          total + detail.amount,
        0
      );


    this.totalDiscount =
      this.orderDetails.reduce(
        (total, detail) =>
          total + detail.discountAmount,
        0
      );


    this.grandTotal =
      this.orderDetails.reduce(
        (total, detail) =>
          total + detail.netAmount,
        0
      );
  }


  // Create Order

  createOrder(): void {

    this.clearMessages();


    // Order Code validation
    if (!this.orderCode.trim()) {

      this.errorMessage =
        'Order Code is required.';

      return;
    }


    // Billing Address validation
    if (!this.billingAddress.trim()) {

      this.errorMessage =
        'Billing Address is required.';

      return;
    }


    // Shipping Address validation
    if (!this.shippingAddress.trim()) {

      this.errorMessage =
        'Shipping Address is required.';

      return;
    }


    // Order Details validation
    if (this.orderDetails.length === 0) {

      this.errorMessage =
        'Please add at least one product.';

      return;
    }


    // Create Order object
    const order: Order = {

      id: 0,

      orderCode: this.orderCode.trim(),

      orderDate: this.orderDate,

      subTotal: this.subTotal,

      totalDiscount: this.totalDiscount,

      grandTotal: this.grandTotal,

      remark: this.remark,

      billingAddress: this.billingAddress.trim(),

      shippingAddress: this.shippingAddress.trim(),

      orderDetails: this.orderDetails

    };


    console.log(
      'Order Request:',
      order
    );


    // Save Order API
    this.orderService.saveOrder(order).subscribe({

      next: (response) => {

        console.log(
          'Save Order Response:',
          response
        );

        this.successMessage =
          'Order created successfully.';

        // Clear form
        this.clearOrder();
      },

      error: (error) => {

        console.error(
          'Save Order API Error:',
          error
        );

        this.errorMessage =
          error.error?.message ??
          'Unable to create order.';
      }

    });
  }


  // Clear Order

  clearOrder(): void {

    this.orderCode = '';

    this.orderDate = new Date()
      .toISOString()
      .substring(0, 10);

    this.billingAddress = '';

    this.shippingAddress = '';

    this.remark = '';

    this.orderDetails = [];

    this.subTotal = 0;

    this.totalDiscount = 0;

    this.grandTotal = 0;

    this.selectedProductId = 0;

    this.quantity = 1;

    this.selectedDiscountId = 0;

    this.currentDetail = null;
  }


  // Clear Messages

  clearMessages(): void {

    this.successMessage = '';

    this.errorMessage = '';
  }

}