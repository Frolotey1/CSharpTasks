namespace Shop;
using System;

public struct Client {
        private int code;
	private string fullName;
        private string address;
        private string phone;
	private int ordersCount;
        private decimal totalOrdersSum;
        private ClientType type;
        
        private string FullName {
	    get {return fullName;}
	    set {fullName = value;}
	}
        private string Address {
	    get {return address;}
	    set {address = value;}
	}
        private string Phone {
	    get {return phone;}
	    set {phone = value;}
	}
	private int Code {
	    get {return code;}
	    set {code = value;}
	}
        private int OrdersCount {
	    get {return ordersCount;}
	    set {ordersCount = value;}
	}
	private decimal TotalOrdersSum {
	    get {return totalOrdersSum;}
	    set {totalOrdersSum = value;}
	}
	private ClientType Type {
	    get {return type;}
	    set {type = value;}
	}
	public void UpdateOrderStats(decimal orderSum) {
	    OrdersCount++;
	    TotalOrdersSum += orderSum;
 	}
	public void Run() {
	    Console.WriteLine("Нажмите на клавишу для запуска программы: ");
	    Console.ReadKey();

	    Console.Write("Введите имя клиента: ");
	    FullName = Console.ReadLine();
	    Console.Write("Введите адрес клиента: ");
	    Address = Console.ReadLine();
	    Console.Write("Введите номер телефона клиента: ");
	    Phone = Console.ReadLine();
	    Console.Write("Введите код клиента: ");
	    Code = int.Parse(Console.ReadLine());
	    Console.Write("Введите количество товаров, осуществленных клиентом: ");
	    OrdersCount = int.Parse(Console.ReadLine());
	    Console.Write("Введите общую сумму заказов клиента: ");
	    TotalOrdersSum = decimal.Parse(Console.ReadLine());

	    ClientType[] types = new ClientType[]{ClientType.Regular,ClientType.Silver,ClientType.Gold,ClientType.Platinum,ClientType.VIP};

	    Console.Write("1) Постоянный\n2) Серебро\n3) Золото\n4) Платинум\n5) Вип\nВыберите тип клиента: ");
	    int selectClient = int.Parse(Console.ReadLine());

	    switch(selectClient) {
		case int c when c >= 1 && c <= 5:
		    Type = types[c - 1];
		    break;
		default:
		    Console.WriteLine("Такой тип клиента не существует");
		    break;
	    }

	    if(Type == null) {
		Console.WriteLine("Невозможно вывести информацию о клиенте без его стажа");
		return;
	    }

	    Console.WriteLine($"Имя клиента: {FullName} | Адрес клиента: {Address} | Телефон: {Phone} | Код клиента: {Code} | Осуществимые клиентом товары: {OrdersCount} | Общая сумма заказов клиента: {TotalOrdersSum} | Тип клиента: {Type}");
	}
}
