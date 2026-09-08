export interface OrderDetail {
  id: number;
  orderId: number;
  productId: number;
  productName?: string;
  quantity: number;
  rate: number;
  amount: number;
  discountId: number;
  discountType?: string;
  discountAmount: number;
  netAmount: number;
}