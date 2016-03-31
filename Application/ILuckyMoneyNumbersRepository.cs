using Business;

namespace Application
{
    public interface ILuckyMoneyNumbersRepository
    {
        System.Collections.Generic.List<Business.LuckyMoneyNumbers> GetLuckyMoneyNumbers();
    }
}
