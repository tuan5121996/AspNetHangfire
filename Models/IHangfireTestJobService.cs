namespace AspNetHangfire.Models
{
    public interface IHangfireTestJobService
    {
        void AddFireAndForgetJob(); //Fire-and-forgot job: thực thi 1 lần và gần như ngay lập tức sau khi khởi tạo.
        void AddReccuringJob(); //Recurring job: Lặp lại công việc nhiều lần dựa trên CRON schedule.
        void AddDelayedJob(); //Delayed job: Thực thi 1 lần sau 1 khoảng thời gian quy định
        void AddContinuationJob(); //Continuations: Tiếp tục thực thi công việc sau khi công việc trước đó thực hiện xong.
    }
}
