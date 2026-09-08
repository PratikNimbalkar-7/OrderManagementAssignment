import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Product } from '../Models/product.Model';
import { Discount } from '../Models/discount.model';
import { OrderDetail } from '../Models/order-detail.model';
import { Order } from '../Models/order.model';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {

    return this.http.get<Product[]>(
      `${this.apiUrl}/Order/products`
    );
  }


  getProductById(id: number): Observable<Product> {

    return this.http.get<Product>(
      `${this.apiUrl}/Order/products/${id}`
    );
  }


  getDiscounts(): Observable<Discount[]> {

    return this.http.get<Discount[]>(
      `${this.apiUrl}/Order/discounts`
    );
  }


  getDiscountById(id: number): Observable<Discount> {

    return this.http.get<Discount>(
      `${this.apiUrl}/Order/discounts/${id}`
    );
  }


  calculateDetail(
    productId: number,
    quantity: number,
    discountId: number
  ): Observable<OrderDetail> {

    const request = {
      productId: productId,
      quantity: quantity,
      discountId: discountId
    };

    return this.http.post<OrderDetail>(
      `${this.apiUrl}/Order/calculate-detail`,
      request
    );
  }

  saveOrder(order: Order): Observable<any> {

    return this.http.post<any>(
      `${this.apiUrl}/Order`,
      order
    );
  }


  getOrderByCode(orderCode: string): Observable<Order> {

    return this.http.get<Order>(
      `${this.apiUrl}/Order/${orderCode}`
    );
  }
}