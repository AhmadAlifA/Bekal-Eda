using Framework.Core.Enums;
using LookUp.Domain.Entities;

namespace LookUp.Test.MockData;
public class AttributeMockData
{
    public static List<AttributeEntity> GetAttributes()
    {
        return new List<AttributeEntity>{
            new AttributeEntity
            {
                Id = new Guid("4C1AD8AD-C085-4E58-81FB-ECE249DD2F4D"),
                Type = AttributeTypeEnum.Number,
                Unit = "Pieces",
                Status = RecordStatusEnum.Active
            },
            new AttributeEntity
            {
                Id = new Guid("8DFBDA43-8FA0-4172-91E6-EFE3DD0D60EA"),
                Type = AttributeTypeEnum.Number,
                Unit = "Package",
                Status = RecordStatusEnum.InActive
            }
        };
    }
}
