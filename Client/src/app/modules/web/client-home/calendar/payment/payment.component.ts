import { Component, Input } from '@angular/core';
import { Basket, BasketItem } from '../models/basket.model';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent {
  @Input() basket?: Basket;
  

  constructor(private basketService: BasketService) {}
}
