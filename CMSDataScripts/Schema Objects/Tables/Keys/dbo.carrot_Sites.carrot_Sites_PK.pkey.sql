﻿ALTER TABLE [dbo].[carrot_Sites]
    ADD CONSTRAINT [carrot_Sites_PK] PRIMARY KEY CLUSTERED ([SiteID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
