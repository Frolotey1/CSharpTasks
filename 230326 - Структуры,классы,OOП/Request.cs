namespace Shop;
using System;

public struct Request {
        public uint orderCode;
        public Client client;
        public DateTime orderDate;
        public RequestItem[] items;
        public PayType paymentType;
        
        private uint OrderCode {
            get { return orderCode; }
            set { orderCode = value; }
        }
        
        private Client Client {
            get { return client; }
            set { client = value; }
        }
        
        private DateTime OrderDate {
            get { return orderDate; }
            set { orderDate = value; }
        }
        
        private RequestItem[] Items {
            get { return items; }
            set { items = value; }
        }
        
        private PayType PaymentType {
            get { return paymentType; }
            set { paymentType = value; }
        }
        
        public decimal OrderSum {
            get {
                decimal sum = 0;
                if (Items != null) {
                    foreach (var item in Items) {
                        sum += item.GetTotalPrice();
                    }
                }
                return sum;
            }
        }
        
        public void Run() {
            Console.Write("Введите код заказа: ");
            OrderCode = uint.Parse(Console.ReadLine());
            
            Client = new Client();
            Client.Run();
            
            OrderDate = DateTime.Now;
            
            Console.WriteLine("Введите количество позиций в заказе: ");
            int itemCount = int.Parse(Console.ReadLine());
            
            Items = new RequestItem[itemCount];
            for (int i = 0; i < itemCount; i++) {
                Console.WriteLine($" Позиция: {i + 1}");
                RequestItem item = new RequestItem();
                item.Run();
                Items[i] = item;
            }
            
            Console.WriteLine("1) Наличные\n2) Банковская карта\n3) Кредит\n4) Рассрочка\n5) Онлайн-платеж\nВыберите форму оплаты: ");
            Console.Write("Выберите опцию: ");
            int paySelect = int.Parse(Console.ReadLine());
            
            switch(paySelect) {
                case 1:
                    PaymentType = PayType.Cash;
                    break;
                case 2:
                    PaymentType = PayType.Card;
                    break;
                case 3:
                    PaymentType = PayType.Credit;
                    break;
                case 4:
                    PaymentType = PayType.Installment;
                    break;
                case 5:
                    PaymentType = PayType.Online;
                    break;
                default:
                    Console.WriteLine("Такая форма оплаты не существует");
                    break;
            }

	    if(PaymentType == null) {
		Console.WriteLine("Невозможно вывести информацию о клиенте");
	    }
         
            Client.UpdateOrderStats(OrderSum);

            Console.WriteLine("ИНФОРМАЦИЯ О ЗАКАЗЕ");
            Console.WriteLine($"Код заказа: {OrderCode}");
            Console.WriteLine($"Дата заказа: {OrderDate:d}");
            Console.WriteLine($"Форма оплаты: {PaymentType}");
            Console.WriteLine($"Сумма заказа: {OrderSum:C}");
            
            Console.WriteLine("Состав заказа");
            for (int i = 0; i < Items.Length; i++) {
                Console.WriteLine($"{i + 1}. Товар: {Items[i].product.name}, " +
                                $"Количество: {Items[i].quantity}, " +
                                $"Стоимость: {Items[i].product.price * Items[i].quantity:C}");
            }
        }
    }
    
