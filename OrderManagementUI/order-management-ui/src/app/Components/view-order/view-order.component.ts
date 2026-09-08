import { Component } from '@angular/core';

import { Order } from '../../Models/order.model';
import { OrderService } from '../../Services/order.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.css']
})
export class ViewOrderComponent {


  orderCode: string = '';

  order: Order | null = null;

  errorMessage: string = '';

  successMessage: string = '';

  constructor(
    private orderService: OrderService
  ) {}

  searchOrder(): void {

    // Clear previous messages
    this.errorMessage = '';

    this.successMessage = '';

    // Clear previous order
    this.order = null;


    // Validate Order Code
    if (!this.orderCode.trim()) {

      this.errorMessage =
        'Please enter Order Code.';

      return;
    }


    // API Call
    this.orderService
      .getOrderByCode(this.orderCode.trim())
      .subscribe({

        next: (response: Order) => {

          console.log(
            'Order Response:',
            response
          );

          this.order = response;

          this.successMessage =
            'Order found successfully.';
        },


        error: (error) => {

          console.error(
            'Get Order API Error:',
            error
          );

          this.errorMessage =
            error.error?.message ??
            'Order not found.';
        }

      });
  }

  clearSearch(): void {

    this.orderCode = '';

    this.order = null;

    this.errorMessage = '';

    this.successMessage = '';
  }

}