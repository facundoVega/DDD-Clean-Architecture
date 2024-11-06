using System.Text;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.SearchVehicles;
using CleanArchitecture.Applications.Abstractions.Data;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CleanArchitecture.Application.Vehicles.ReportVehiclePdf;

internal sealed class ReportVehiclePdfQueryHandler
: IQueryHandler<ReportVehiclePdfQuery, Document>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ReportVehiclePdfQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory
    )
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Document>> Handle(
        ReportVehiclePdfQuery request, 
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();


        var builder =new StringBuilder("""
            SELECT
                v.Id as Id,
                v.model as Model,
                v.vin as Vin,
                v.price_amount as Price
            FROM vehicles AS v
        """);

        var search = string.Empty;
        var where = string.Empty;

        if(!string.IsNullOrEmpty(request.Model))
        {
            search = "%" + request.Model + "%";
            where = $" WHERE v.model LIKE @Search";
            builder.AppendLine(where);
        }

        builder.AppendLine(" ORDER BY v.model");

        var vehicles = await connection.QueryAsync<VehicleResponse>(
            builder.ToString(),
            new {
                Search = search
            }
        );

        var report = Document.Create(container => {
            container.Page(page => {
                page.Margin(50);
                page.Size(PageSizes.A4.Landscape());
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .AlignCenter()
                    .Text("Highline Vehicles")
                    .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                page.Content().Padding(25)
                    .Table(table => {
                        table.ColumnsDefinition(columns => {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header => {
                            header.Cell().Element(CellStyle).Text("Model");
                            header.Cell().Element(CellStyle).Text("Vin");
                            header.Cell().Element(CellStyle).AlignRight().Text("Price");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                .DefaultTextStyle(
                                    x => x.SemiBold())
                                    .PaddingVertical(5).
                                    BorderBottom(1).
                                    BorderColor(Colors.Black);
                            }
                        });

                        foreach(var vehicle in vehicles)
                        {
                            table.Cell().Element(CellStyle).Text(vehicle.Model);
                            table.Cell().Element(CellStyle).Text(vehicle.Vin);
                            table.Cell().Element(CellStyle).AlignRight().Text($"${ vehicle.Price }");


                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .PaddingVertical(5);
                            }
                        }

                    });
            });

        });
    
        return report;
    }
}
