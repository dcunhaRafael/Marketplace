using Dapper;
using Domain.Entities;
using Domain.Payload;
using Domain.Util.Extensions;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class LatePaymentRepository : BaseRepository, ILatePaymentRepository {

        public LatePaymentRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public IList<LatePaymentSlip> ListLatePaymentSlip(string brokerLegacyCode, string takerLegacyCode, string insuredLegacyCode, string productLegacyCode,
            long? policyNumber, int? endorsementNumber, int? installmentNumber, decimal? premiumValue, string ourNumber,
            DateTime? fromDate, DateTime? toDate) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM ID0025_parcelas_pendentes")
                .AppendLine("WHERE 1 = 1")
                .AppendLine("AND (@BrokerLegacyCode IS NULL OR id_pessoa_corretor = @BrokerLegacyCode)")
                .AppendLine("AND (@TakerLegacyCode IS NULL OR id_pessoa_tomador = @TakerLegacyCode)")
                //.AppendLine("AND (@InsuredLegacyCode IS NULL OR XXXX = @InsuredLegacyCode)")
                .AppendLine("AND (@ProductLegacyCode IS NULL OR cd_produto = @ProductLegacyCode)")
                .AppendLine("AND (@PolicyNumber IS NULL OR cd_apolice = @PolicyNumber)")
                .AppendLine("AND (@EndorsementNumber IS NULL OR nr_endosso = @EndorsementNumber)")
                .AppendLine("AND (@InstallmentNumber IS NULL OR nr_parcela = @InstallmentNumber)")
                .AppendLine("AND (@PremiumValue IS NULL OR @PremiumValue = 0 OR vl_total = @PremiumValue)")
                //.AppendLine("AND (@OurNumber IS NULL OR XXXX = @OurNumber)")
                //.AppendLine("AND (@FromDate IS NULL OR XXXX >= @FromDate)")
                //.AppendLine("AND (@ToDate IS NULL OR XXXX <= @ToDate)")
                .AppendLine("ORDER BY nm_pessoa_sub");

            using var dbConn = base.GetStageDbConnection();
            var entities = dbConn.Query<ID0025_parcelas_pendentes>(
                query.ToString(),
                param: new {
                    BrokerLegacyCode = brokerLegacyCode,
                    TakerLegacyCode = takerLegacyCode,
                    InsuredLegacyCode = insuredLegacyCode,
                    ProductLegacyCode = productLegacyCode,
                    PolicyNumber = policyNumber,
                    EndorsementNumber = endorsementNumber,
                    InstallmentNumber = installmentNumber,
                    PremiumValue = premiumValue,
                    OurNumber = ourNumber,
                    FromDate = fromDate,
                    ToDate = toDate
                }).ToList();

            var payloads = new List<LatePaymentSlip>();
            foreach (var item in entities) {
                payloads.Add(new LatePaymentSlip() {
                    Id = item.id_parcela_pendente,
                    ProductName = item.nm_produto,
                    BrokerName = item.nm_pessoa_corretor,
                    BrokerSusepCode = "",
                    BrokerLegacyCode = item.id_pessoa_corretor?.ToString(),
                    BrokerDocument = item.nr_cnpj_cpf_corretor,
                    TakerName = item.nm_pessoa_tomador,
                    TakerDocument = item.nr_cnpj_cpf_tomador,
                    TakerAddress = new Domain.Payload.PersonAddress() {
                        StreetName = item.end_tomador,
                        Number = item.nr_rua_endereco_1,
                        Complement = item.nm_complemento_1,
                        District = item.nm_bairro_1,
                        City = new Domain.Payload.City() {
                            Name = item.nm_cidade_1,
                            State = new Domain.Payload.State() {
                                Name = item.nm_estado_1
                            }
                        },
                        ZipCode = int.Parse(item.nm_cep_1?.KeepNumbersOnly() ?? "0")
                    },
                    InsuredName = item.nm_pessoa_sub,
                    InsuredDocument = item.nr_cnpj_cpf_segurado,
                    InsuredAddress = new Domain.Payload.PersonAddress() {
                        StreetName = item.end_segurado,
                        Number = item.nr_rua_endereco_2,
                        Complement = item.nm_complemento_2,
                        District = item.nm_bairro_2,
                        City = new Domain.Payload.City() {
                            Name = item.nm_cidade_2,
                            State = new Domain.Payload.State() {
                                Name = item.nm_estado_2
                            }
                        },
                        ZipCode = int.Parse(item.nm_cep_2?.KeepNumbersOnly() ?? "0")
                    },
                    BusinessId = item.id_ramo,
                    BusinessName = item.nm_ramo,
                    PolicyNumber = item.cd_apolice,
                    ProposalNumber = item.cd_proposta,
                    EndorsementNumber = item.nr_endosso,
                    EndorsementId = item.id_endosso,
                    InstallmentNumber = item.nr_parcela,
                    InstallmentCount = item.qt_parcela,
                    PaymentMethodName = item.nm_forma_pagamento,
                    DueDate = DateTime.ParseExact(item.dt_vencimento, "dd/MM/yyyy", null),
                    TariffPremiumValue = item.vl_tarifario,
                    //NetPremiumValue = item,
                    //AdditionalFractionation = item,
                    CostValue = item.vl_custo,
                    IofValue = item.vl_iof,
                    TotalPremiumValue = item.vl_total,
                    LateDays = item.nr_dias_atraso,
                    OurNumber = item.nr_nosso_numero,
                    //NewDueDate = item,
                    //InterestAmount = item.juros,
                    InterestPercent = item.qt_juros,
                    //FineAmount = item.multa,
                    //DiscountAmount = item.desconto,
                    DiscountPercent = item.Qt_desconto,
                    //NewTotalValue = item,
                    //NewLateDays = item,
                    //NewOurNumber = item,
                    BarcodeNumber = item.nr_numero_codigo_barra,
                    BarcodeDigitableLine = item.nm_linha_digitavel,
                });
            }

            return payloads;
        }

        public LatePaymentSlip GetLatePaymentSlip(string ourNumber) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM ID0025_parcelas_pendentes")
                .AppendLine("WHERE nr_nosso_numero = @OurNumber");

            using var dbConn = base.GetStageDbConnection();
            var item = dbConn.Query<ID0025_parcelas_pendentes>(
                query.ToString(),
                param: new {
                    OurNumber = ourNumber,
                }).FirstOrDefault();

            var payload = new LatePaymentSlip() {
                ProductName = item.nm_produto,
                BrokerName = item.nm_pessoa_corretor,
                BrokerSusepCode = item.cd_susep_re,
                BrokerLegacyCode = item.id_pessoa_corretor?.ToString(),
                BrokerDocument = item.nr_cnpj_cpf_corretor,
                TakerName = item.nm_pessoa_tomador,
                TakerDocument = item.nr_cnpj_cpf_tomador,
                InsuredName = item.nm_pessoa_sub,
                InsuredDocument = item.nr_cnpj_cpf_segurado,
                PolicyNumber = item.cd_apolice,
                EndorsementNumber = item.nr_endosso,
                EndorsementId = item.id_endosso,
                InstallmentNumber = item.nr_parcela,
                PaymentMethodName = item.nm_forma_pagamento,
                DueDate = DateTime.ParseExact(item.dt_vencimento, "dd/MM/yyyy", null),
                TariffPremiumValue = item.vl_tarifario,
                //NetPremiumValue = item,
                //AdditionalFractionation = item,
                CostValue = item.vl_custo,
                IofValue = item.vl_iof,
                TotalPremiumValue = item.vl_total,
                LateDays = item.nr_dias_atraso,
                OurNumber = item.nr_nosso_numero,
                //NewDueDate = item,
                //InterestAmount = item.juros,
                //FineAmount = item.multa,
                //DiscountAmount = item.desconto,
                //NewTotalValue = item,
                //NewLateDays = item,
                //NewOurNumber = item
            };

            return payload;
        }
    }
}
