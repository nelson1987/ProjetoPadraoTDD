SELECT TOP 100 matforn.* FROM wfm_dsv.dbo.WFM_MATERIAL_FORNECEDOR matforn 
INNER JOIN wfm_dsv.dbo.WFM_FORNECEDORES forn ON matforn.COD_FORNECEDOR = forn.COD_FORNECEDOR
WHERE matforn.COD_FORNECEDOR IN(31094, 31101,31122)
--WHERE COD_MATERIAL = '001307276'
ORDER BY COD_FORNECEDOR;

SELECT TOP 100 pdm.* FROM wfm_dsv.dbo.TRD_MATERIAL mat
INNER JOIN wfm_dsv.dbo.TRD_PDM pdm ON mat.COD_PDM = pdm.COD_PDM
WHERE mat.COD_MATERIAL IN('001307276','000000007')

SELECT  TOP 100 * FROM wfm_dsv.dbo.TRD_PDM ORDER BY COD_PDM

000000007
000000014
000000040
/*
INSERT INTO wfm_dsv.dbo.WFM_MATERIAL_FORNECEDOR (COD_FORNECEDOR, COD_MATERIAL, COD_CLIENTE )
VALUES(31094,'000000007', 'AMIL')
*/



 WHERE matforn.COD_FORNECEDOR = '25593';

SELECT TOP 10 * FROM WFM_FORNECEDORES WHERE COD_IDIOMA = 'PRT'
SELECT TOP 10 * FROM NOME
SELECT TOP 10 COD_FORNECEDOR, RAZAO_SOCIAL FROM WFM_FORNECEDORES --WHERE cod_idioma = 'PRT' 
SELECT TOP 10 *  FROM wfm_dsv.dbo.WFM_FORNECEDORES

SELECT COUNT(COD_FORNECEDOR) As Ocorrencias, COD_MATERIAL
FROM wfm_dsv.dbo.WFM_MATERIAL_FORNECEDOR
GROUP BY COD_MATERIAL
HAVING COUNT(COD_FORNECEDOR) > 1
COD_MATERIAL = '001307270'
-- FERROUS
SELECT * FROM wfm_dsv.dbo.WFM_MATERIAL_FORNECEDOR WHERE ID_MATERIAL_FORNECEDOR = 133078;
--UPDATE wfm_dsv.dbo.WFM_MATERIAL_FORNECEDOR
--SET COD_CLIENTE = 'FERROUS'
--WHERE ID_MATERIAL_FORNECEDOR = 133078;				-- FERROUS
--INSERT INTO 

