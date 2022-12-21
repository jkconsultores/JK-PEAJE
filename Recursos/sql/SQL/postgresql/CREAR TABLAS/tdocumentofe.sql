CREATE TABLE IF NOT EXISTS public.tdocumentofe
(
    numerodocumentoemisor character(12) COLLATE pg_catalog."default" NOT NULL,
    serienumero character(14) COLLATE pg_catalog."default" NOT NULL,
    tipodocumento character(2) COLLATE pg_catalog."default" NOT NULL,
    PRIMARY KEY (numerodocumentoemisor, serienumero, tipodocumento)
)