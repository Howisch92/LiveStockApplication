import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-stockmarket',
  templateUrl: './stockmarket.component.html'
})
export class StockMarketComponent {
  public stocks: StocksViewModel[]
  private _hubConnection: HubConnection;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log(this._hubConnection)
    http.get<StocksViewModel[]>(baseUrl + 'stock/OpenStockmarket').subscribe(result => {
      this.stocks = result;
    }, error => console.error(error));
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }


  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("/StockmarketHub").build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
      });
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('UpdateStockmarket', (data: any) => {
      this.stocks = data;
    });
  }
  ngOnDestroy() { this._hubConnection.stop() }
}

interface StocksViewModel {
  name: string;
  bidPrice: number;
  askPrice: number;
  changed: boolean;
}
