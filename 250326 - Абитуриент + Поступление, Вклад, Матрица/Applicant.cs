namespace Project;
using System;
using System.Globalization;

public class Applicant {
    private string Name {get; set;}
    private string Lastname {get; set;}
    private string Surname {get; set;}
    public double AverageGrade {get; set;}
    private double PersonalAchievement {get; set;}
    private string Date {get; set;}
    public Applicant(string name, string lastname, string surname, double averageGrade, double personalAchievement, string date) {
	Name = name;
	Lastname = lastname;
	Surname = surname;
	AverageGrade = averageGrade;
	PersonalAchievement = personalAchievement;
	Date = date;
    }
    public static bool operator<(Applicant first, Applicant second) {
	return first.AverageGrade < second.AverageGrade;
    }
    public static bool operator>(Applicant first, Applicant second) {
	return first.AverageGrade > second.AverageGrade;
    }
    private static bool HasFirstApplicantMorePersonalAchievement(Applicant first, Applicant second) {
	return first.PersonalAchievement > second.PersonalAchievement;
    }
    private static bool HasSecondApplicantMorePersonalAchievement(Applicant first, Applicant second) {
	return first.PersonalAchievement < second.PersonalAchievement;
    }
    public static void Run() {
        Applicant applicant = null, applicant2 = null;

	uint fullData = 0;

	while(fullData < 2) {
	    Console.Write($"Введите имя {fullData + 1} студента: ");
	    string? name = Console.ReadLine();
	    Console.Write($"Введите фамилию {fullData + 1} студента: ");
	    string? lastname = Console.ReadLine();
	    Console.Write($"Введите отчество {fullData + 1} студента: ");
	    string? surname = Console.ReadLine();
	    Console.Write($"Введите средний балл аттестата {fullData + 1} студента: ");
	    double averageGrade = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
	    Console.Write($"Введите балл за личные достижения {fullData + 1} студента: ");
	    double personalAchievement = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
	    Console.Write($"Введите дату подачи документов для {fullData + 1} студента: ");
	    string? date = Console.ReadLine();

	    if(fullData == 0)
		applicant = new Applicant(name,lastname,surname,averageGrade,personalAchievement,date);	    
	    else
		applicant2 = new Applicant(name,lastname,surname,averageGrade,personalAchievement,date);	    

	    fullData++;
	}

	Applicant[] applicants = new Applicant[2]{applicant,applicant2};

	foreach(var get in applicants) {
	    Console.WriteLine($"Имя: {get.Name} | Фамилия: {get.Lastname} | Отчество: {get.Surname} | Средний бапл аттестата: {get.AverageGrade} | Балл за личные достижения: {get.PersonalAchievement} | Дата подачи документов: {get.Date}");
	}

	if(applicant > applicant2) {
	    Console.WriteLine("Средний балл 1 абитуриента, больше чем у 2 абитуриента");
	} else if(applicant < applicant2) {
	    Console.WriteLine("Средний балл 2 абитуриента, больше чем у 1 абитуриента");
	} else {
	    if(Applicant.HasFirstApplicantMorePersonalAchievement(applicant,applicant2)) {
		Console.WriteLine("Баллов за личные достижения у 1 абитуриента больше, чем у 2 абитуриента");
	    } else if(Applicant.HasSecondApplicantMorePersonalAchievement(applicant,applicant2)) {
		Console.WriteLine("Баллов за личные достижения у 2 абитуриента больше, чем у 1 абитуриента");
	    } else {
	        Console.Write("1) 1 абитуриент\n2) 2 абитуриент\nВыберите абитуриента для проверки на соответствие с проходным баллом: ");
		int selectApplicant = int.Parse(Console.ReadLine());

		switch(selectApplicant) {
		    case int check when check >= 1 && check <= 2:
			if(Admission.IsEqualToPassingScore(applicants[check - 1])) {
			    Console.WriteLine($"Балл аттестата {check} абитуриента соответствует проходному баллу");
			} else {
			    Console.WriteLine($"Балл аттестата {check} абитуриента не соответствует проходному баллу");
			}
			break;
		    default:
			Console.WriteLine("Такой абитуриент не найден");
			break;
		}
	    }
	}
    }
}
