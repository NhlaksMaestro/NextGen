﻿------Rates
--INSERT INTO [dbo].[Rates] ([Rate], [From], [To])
--VALUES
--    (10, 0, '8350'),
--    (15, 8351, '33950 (0 to 8350 at 10%)'),
--    (25, 33951, '82250 (8351 to 33950 - 15%)'),
--    (28, 82251, '171550 (33951 - 82250 25%)'),
--    (33, 171551, '372950 (82251 - 171550 28%)'),
--    (35, 372951, '- (171551-372950 33%)');

------PostalCodes
--INSERT INTO [dbo].[PostalCodes] ([PostalCode], [TaxCalculationType])
--VALUES
--    ('7441', 'Progressive'),
--    ('A100', 'Flat Value'),
--    ('7000', 'Flat Rate'),
--         ('1000', 'Progressive');