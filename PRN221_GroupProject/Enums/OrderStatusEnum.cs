namespace PRN221_GroupProject.Enums
{
    public enum OrderStatusEnum
    {
        Pending, //customer just placed an order
        Approved, //admin check whether product in-stock or not and approved
        Processing, //store will prepare the package
        Shipped, //once customer receive the package
        Cancelled, //admin or customer can canncel the order
        Refunded, //if any product damage during shipping stage 
    }
}
