import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbDate, NgbDatepicker, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { TimeModel } from './models/time.model';
import { BasketItem } from './models/basket.model';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit{
  model: any;
  minDate: NgbDateStruct | any;
  selectedDate: NgbDateStruct;
  step: string = "date";
  timeSelection: FormGroup;
  battonValue: string = "Next & Time Selection";

  private item: BasketItem = {
    id: 1,
    price: 60,
    productName: 'meet',
    quantity: 1,
  }

  constructor(private basketService: BasketService) {
    this.minDate = new NgbDate (
      new Date().getFullYear(),
      new Date().getMonth() + 1,
      new Date().getDate() + 1
    )
    this.selectedDate = this.minDate;

    this.timeSelection = new FormGroup({
      time: new FormControl<string>("", Validators.required)
    })
  }

  ngOnInit(): void {
  }

  navigate(datepicker: NgbDatepicker, number: number) {
    const { state, calendar } = datepicker;
    datepicker.navigateTo(calendar.getNext(state.firstDate, 'm', number));
  }

  change(obj: NgbDateStruct): void {
    this.selectedDate = obj;
    // console.log(obj.day);
  }

  nextStep(): void {
    switch (this.step) {
      case "time":
        this.step = "personal";
        this.item && this.basketService.addItemsToBasket(this.item);
        break;
      case "date":
        this.step = "time";
        this.battonValue = "Next & Personal Infromation";
        break;
      case "personal":
        this.step = "payment";
        this.battonValue = "Proceed to Payment";
        break;
      // TODO: Book Consulation
    }
  }

  time: TimeModel[] = [
    { id: 9, value: "09:00", view: "09:00 - 10:00" },
    { id: 10, value: "10:00", view: "10:00 - 11:00" },
    { id: 11, value: "11:00", view: "11:00 - 12:00" },
    { id: 12, value: "12:00", view: "12:00 - 13:00" },
    { id: 13, value: "13:00", view: "13:00 - 14:00" },
    { id: 14, value: "14:00", view: "14:00 - 15:00" },
    { id: 15, value: "15:00", view: "15:00 - 16:00" },
    { id: 16, value: "16:00", view: "16:00 - 17:00" },
  ]
}
