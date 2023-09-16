# How to run project:

1. Install Docker desktop
2. `docker-compose up --build -d`
3. Start Silo: `cd` Silo project/ `dotnet run` with possibility to run more silos by specifying silo port and gateway port with arguments: `dotnet run <siloPort> <gatewayPort>`
4. Start Client: `cd` Client project/ `dotnet run`
5. After client is started, max throughput service is started too, so be careful.

Tests are mostly for debugging purposes.

# Zadání úkolu

Tento úkol je zaměřen na seznámení se s Microsoft Orleans a .NET 7. Cílem je demonstrovat schopnost implementovat základní cluster v Orleans a možnosti frameworku, které jsou běžné v práci s Orleans.

## Instrukce

1. Forkněte si toto repo na váš vlastní GitHub účet.
2. Nastavte konfigurace editoru a Gitu v repozitáři podle vašich preferencí.
3. Vytvořte malý projekt v .NET 7 a Orleans 7.
4. Zveřejněte svůj kód v repozitáři a pošlete nám odkaz na váš fork.

## Zadání pro projekt Orleans 7

Vaším úkolem je vytvořit demonstrační projekt s využitím Microsoft Orleans ve verzi 7. Zde jsou konkrétní úkoly, které by váš projekt měl splnit:

1. **Orleans Silo**: Nastavte prostředí pro Orleans Silo. Toto je základní krok, který je nezbytný pro další práci s Orleans.

2. **POCO Grain**: Vytvořte jednoduchý POCO grain, který neuchovává stav mezi voláními. POCO grainy v Orleans 7 nevyžadují dědění od třídy Grain.

3. **Stavový grain (bez persistence)**: Vytvořte stavový grain, který udržuje stav mezi voláními, ale není persistován.

4. **Stavový grain (s persistencí)**: Implementujte stavový grain, který svůj stav persistuje. Můžete využít Azure CosmosDB nebo Azure Blob Storage pro persistenci stavu grainu.

5. **Více stavů v jednom grainu (přes facets)**: Demonstrujte, jak může grain obsahovat více stavů pomocí Orleans grain facets.

6. **Bezstavový grain (stateless)**: Vytvořte bezstavový grain, který má maximální propustnost v možnostech volání metod grainu.

7. **Distributed Transactions**: Vytvořte přiklad grainů, které ukazují, jak v Orleans provádět distribuované transakce. Mohlo by to být například jednoduché bankovní rozhraní, kde můžete převádět peníze mezi účty a musíte se ujistit, že transakce jsou konzistentní.

8. **Timers and Reminders**: Vytvořte příklad grainu, který ukazuje, jak používat timers a reminders v Orleans.

9. **Reentrant Grains**: Vytvořte grain, který demonstruje použití vlatnosti reentrant v Orleans.

10. **Orleans Dashboard**: Integrujte svůj projekt s Orleans Dashboard pro lepší monitorování a ladění.

11. **Hostovaná služba, která volání 'bezstavový grain (stateless)'**: Vytvořte co-hostovanou službu v ramci stejného runtime za pomoci 'IHostedService', ve které budete maximalní možnou rychlostí volat bezstavový grain (stateless). Ověřte si počty volání za sekundu v Dashbordu.

12. **Podpora více Sil**: Rozšiřte svůj projekt tak, aby podporoval více sil. Pro demonstraci stačí dvě sila.

13. **Virtuální streamy**: Implementujte virtuální streamy v Orleans pro asynchronní komunikaci mezi grainy.

14. **Pub/Sub vzor**: Využijte Orleans pro implementaci publish/subscribe vzoru pro asynchronní zpracování událostí.

15. **Agregační vzor**: Demonstrujte, jak lze v Orleans implementovat agregační vzor.

Vaším cílem je ukázat, jak efektivně využívat Orleans 7 pro distribuované výpočty a jak správně pracovat se stavovými a bezstavovými grainy, virtuálními streamy a dalšími funkcemi Orleans. Výsledný projekt by měl být dobře strukturovaný a komentovaný, aby bylo možné pochopit jednotlivé kroky a jejich účel.

## Poznámky

- Můžete použít jakoukoli databázi podporovanou Orleans pro ukládání stavu grainu, pokud je to pro váš use-case potřeba.
- Jakýkoli další kód nebo dokumentace, které považujete za užitečné pro demonstraci vašeho porozumění Orleans a .NET, je vítaný.
- Hodnotit budeme jak správnost a úplnost implementace, tak kvalitu kódu a jeho dokumentace.

## Ostatní

- Orleans [dokumentace](https://learn.microsoft.com/en-us/dotnet/orleans/)
- Kontakt:
  - Jaroslav Urbánek, jaroslav.urbanek@core.cz (kopie na jaroslav.urbanek@packeta.com), +420777007070
