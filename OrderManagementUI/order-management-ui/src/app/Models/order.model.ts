import { OrderDetail } from './order-detail.model';

export interface Order {
  id: number;
  orderCode: string;
  orderDate: string;
  subTotal: number;
  totalDiscount: number;
  grandTotal: number;
  remark: string;
  billingAddress: string;
  shippingAddress: string;
  orderDetails: OrderDetail[];
}