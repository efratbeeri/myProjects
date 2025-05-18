using Microsoft.EntityFrameworkCore;

namespace projectApi.DAL
{
    public class FileHandler
    {
        private readonly OrdersDBContex _ordersDBContex;

        public FileHandler(OrdersDBContex ordersDBContex)
        {
            _ordersDBContex = ordersDBContex;
        }

        // שמירת מידע על המנצחים בקובץ
        public async Task<List<string>> SaveWinnerDocument(int presentId, int userId)
        {
            var presentName = (await _ordersDBContex.Present.FirstOrDefaultAsync(p => p.Id == presentId))?.Name;
            var userName = (await _ordersDBContex.User.FirstOrDefaultAsync(p => p.Id == userId))?.Name;

            List<string> winners = new List<string>
        {
            $"הזוכה במתנה {presentName} הוא {userName}"
        };

            // שמירה לקובץ (לדוגמה כקובץ טקסט)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "winners.txt");
            await File.AppendAllLinesAsync(filePath, winners);

            return winners;
        }

        // שמירת הכנסות מהמכירות לקובץ
        public async Task<string> SaveRevenueDocument()
        {
            var totalRevenue = _ordersDBContex.Purchases
                .Sum(p => p.Amount * p.Present.Price);

            var revenueMessage = $"סך ההכנסות מהמכירה הינו: {totalRevenue}";

            // שמירה לקובץ (לדוגמה כקובץ טקסט)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "revenue.txt");
            await File.AppendAllTextAsync(filePath, revenueMessage + Environment.NewLine);

            return revenueMessage;
        }
    }

}
