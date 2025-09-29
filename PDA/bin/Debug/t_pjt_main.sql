


with receipt_all as (

    select receipt_a.pjt_code,
           receipt_a.pjt_name,
		   year(receipt_a.ACTUAL_DATE) as stat_year,
           sum(ifnull(receipt_a.ACTUAL_MONEY, 0)) as receipt_MONEY
    from dm_pjt_receipt_detail_a receipt_a
             left join t_cd_dataweek dataweek
                       on DATE(receipt_a.ACTUAL_DATE) = DATE(dataweek.calendr_dte)
    group by receipt_a.pjt_code, receipt_a.pjt_name,year(receipt_a.ACTUAL_DATE)
),
receipt_pro as(
     select receipt_a.pjt_code,
           receipt_a.pjt_name,
           sum(ifnull(receipt_a.ACTUAL_MONEY, 0)) as receipt_MONEY
    from dm_pjt_receipt_detail_a receipt_a
             left join t_cd_dataweek dataweek
                       on DATE(receipt_a.ACTUAL_DATE) = DATE(dataweek.calendr_dte)
    group by receipt_a.pjt_code, receipt_a.pjt_name
),
costdetail_all as (
    select costdetail.pjt_code,
           costdetail.pjt_name,
           costdetail.fee_type,
		   year(costdetail.EFFECTIVE_DATE) as stat_year,
           sum(ifnull(costdetail.AMOUNT, 0)) as cost_AMOUNT
    from dm_pjt_costdetail costdetail
    group by costdetail.pjt_code, costdetail.pjt_name, costdetail.fee_type,year(costdetail.EFFECTIVE_DATE)
    UNION ALL
    select pjtinfo1.pjt_code,
           pjtinfo1.pjt_name,
           'LABOUR'                                 as fee_type,
		   year(workload1.reportday) as stat_year,
           sum(ifnull(workload1.workday, 0) * 1800) as cost_AMOUNT
    from dm_pjt_workload workload1
             LEFT JOIN dm_pjt_pjtinfo pjtinfo1 ON pjtinfo1.pjt_code = workload1.pjt_code
          AND pjtinfo1.pjt_name IS NOT NULL
    group by pjtinfo1.pjt_code, pjtinfo1.pjt_name,year(workload1.reportday)
		
),
							
costdetail_all_SUM AS (
	SELECT 
	    costdetail_all1.pjt_code,
        costdetail_all1.pjt_name,
		costdetail_all1.stat_year,
        sum(case
            when fee_type = 'OUTSOURCE' then costdetail_all1.cost_AMOUNT
            else 0 end) as                                                                OUTSOURCE_AMOUNT,
        sum(case
            when fee_type = 'TRAVEL_CHARGE' then costdetail_all1.cost_AMOUNT
            else 0 end) as                                                                TRAVEL_CHARGE_AMOUNT,
        sum(
            case when fee_type = 'LABOUR' then costdetail_all1.cost_AMOUNT else 0 end) as LABOUR_AMOUNT,
        sum(costdetail_all1.cost_AMOUNT) as                                                   cost_AMOUNT
    from costdetail_all costdetail_all1
    group by costdetail_all1.pjt_name, costdetail_all1.pjt_code,costdetail_all1.stat_year
),

costdetail_pro AS (
	SELECT 
	    costdetail_all1.pjt_code,
        costdetail_all1.pjt_name,
        sum(case
            when fee_type = 'OUTSOURCE' then costdetail_all1.cost_AMOUNT
            else 0 end) as                                                                OUTSOURCE_AMOUNT,
        sum(case
            when fee_type = 'TRAVEL_CHARGE' then costdetail_all1.cost_AMOUNT
            else 0 end) as                                                                TRAVEL_CHARGE_AMOUNT,
        sum(
            case when fee_type = 'LABOUR' then costdetail_all1.cost_AMOUNT else 0 end) as LABOUR_AMOUNT,
        sum(costdetail_all1.cost_AMOUNT) as                                                   cost_AMOUNT
    from costdetail_all costdetail_all1
    group by costdetail_all1.pjt_name, costdetail_all1.pjt_code
),
yingyu as (

select 
        pjt_all1.pjt_code  as pjt_code,
        pjt_all1.pjt_name  as pjt_name,  
		ifnull(receipt_all1.receipt_MONEY, 0) - ifnull(cost_all.cost_AMOUNT, 0) as incexp_amount, -- 收支盈余
		ifnull(receipt_pro1.receipt_MONEY, 0) - ifnull(costdetail_pro1.cost_AMOUNT, 0) as incexp_amount_pro -- 项目累计收支盈余		 
			 
from t_pjt_main pjt_all1
         left join receipt_all receipt_all1 on receipt_all1.pjt_code = pjt_all1.pjt_code and receipt_all1.stat_year = YEAR(NOW())
         left join costdetail_all_SUM cost_all on cost_all.pjt_code = pjt_all1.pjt_code and cost_all.stat_year=YEAR(NOW())	 
				 
				 left join receipt_pro receipt_pro1 on receipt_pro1.pjt_code = pjt_all1.pjt_code 
         left join costdetail_pro costdetail_pro1 on costdetail_pro1.pjt_code = pjt_all1.pjt_code
		 
where EXISTS (
    select 1 from dm_pjt_final final where final.pjt_code=pjt_all1.pjt_code
)
),
workday as (	
	select
    load1.pjt_code,
		load1.pjt_name,
    load1.member_code,
		load1.reportday,
		price.PriceDay,
    round((ifnull(price.PriceDay,1800)/1600),2)*load1.workday as workdays
from dm_pjt_workload load1
LEFT JOIN dm_pjt_contra pjt_contra ON pjt_contra.CONTRACT_NO = load1.pjt_code 
left join dm_pjt_pjtinfo pjtinfo on pjtinfo.pjt_code=load1.pjt_code
left join t_cd_member_price price on price.num=load1.member_code and cast(price.feeyear as UNSIGNED)=year(load1.reportday)
where year(load1.reportday)=year(now()) and pjtinfo.pjt_code is not null
	),
	
	workday_sum as (
	
		SELECT
		workday1.pjt_code,				
				round((sum(workday1.workdays)/252),2) AS workday_year
			FROM
				workday workday1
				group by workday1.pjt_code

	),
	
	
	
	total_workday as (
	
	select
    load1.pjt_code,
		load1.pjt_name,
    load1.member_code,
		load1.reportday,
		price.PriceDay,
    round((ifnull(price.PriceDay,1800)/1600),2)*load1.workday as workdays
from dm_pjt_workload load1
LEFT JOIN dm_pjt_contra pjt_contra ON pjt_contra.CONTRACT_NO = load1.pjt_code 
left join dm_pjt_pjtinfo pjtinfo on pjtinfo.pjt_code=load1.pjt_code
left join t_cd_member_price price on price.num=load1.member_code and cast(price.feeyear as UNSIGNED)=year(load1.reportday)
	
	
	),
	
	total_workday_sum as (
	
		SELECT
		workday1.pjt_code,				
				round((sum(workday1.workdays)/252),2) AS workday_year
			FROM
				total_workday workday1
				group by workday1.pjt_code

	),
  financial_yewu as(
select 
pjtinfo.pjt_code,
pjtinfo.pjt_name,
pjtinfo.CONTRACT_MONEY,
case when financial_info3.project_progress is null then (case when pjtinfo.PJT_STS in('2') then 1.00 else null end ) else financial_info3.project_progress end as project_progress
from  dm_pjt_pjtinfo pjtinfo
left join (
    select 
financial_info2.pjt_code,
financial_info2.project_progress
from
(select
    financial_info.pjt_code,
    round((financial_info.project_progress)/100,2) as project_progress,
    row_number() over(PARTITION by financial_info.pjt_code order by financial_info.stat_day desc) as sn 
from dm_pjt_financial_info financial_info
)financial_info2 where financial_info2.sn=1

)financial_info3 on financial_info3.pjt_code=pjtinfo.pjt_code
),
	total_workday_yewu as (
	
	select
    load1.pjt_code,
		load1.pjt_name,
    load1.member_code,
		load1.reportday,
		price.PriceDay,
    ifnull(price.PriceDay,1800)*load1.workday as workdays
from dm_pjt_workload load1
left join t_cd_member_price price on price.num=load1.member_code and cast(price.feeyear as UNSIGNED)=year(load1.reportday)
	
	
	),
	
	total_workday_sum_yewu as (
	
		SELECT
		workday1.pjt_code,				
				round(sum(workday1.workdays),2) AS workday_year
			FROM
				total_workday_yewu workday1
				group by workday1.pjt_code

	)
	
	
	
	
	
	
select
pjt.id as pjt_id,
pjt.pjt_code,
pjt.pjt_name,
pjt.CONTRACT_MONEY as amount,
pjt.pjt_owner,
case when workload2.item_members is null then 0 else workload2.item_members end as item_members, -- 项目人员
case when workload2.un_saturated is null then 0 else workload2.un_saturated end as un_saturated,
null as checked_num,
null as ineligible_num,

case when pjt.CONTRACT_MONEY>0 then round(((ifnull(pjt.CONTRACT_MONEY,0)-(case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end))/ifnull(pjt.CONTRACT_MONEY,0))*100,2) else null end as gross_profit,
null as planned_profit_amt,
null as planned_gross_profit,
'001' as dept_code,
'软件交付中心' as dept_name,
now() as made,
pjt.planned_start_date,
pjt.planned_end_date,
Psm_Pjt2.RealStartDate	 as actual_start_date,
Psm_Pjt2.RealEndDate as actual_end_date,
case when ifnull(members_p2.WORKINGR_DAY,0)<=0 then 0 else  ifnull(pjt.CONTRACT_MONEY,0)/ifnull(members_p2.WORKINGR_DAY,0)  end as per_capita_amount,
case when ifnull(members_p2.WORKINGR_DAY,0)<=0 then 0 else  (ifnull(pjt.CONTRACT_MONEY,0)-ifnull(budgetcost.COST_BUDGET_AMT,0))/ifnull(members_p2.WORKINGR_DAY,0)  end  as per_capita_income,
case when ifnull(workload_year.workday,0)<=0 then 0 else  (ifnull(shoukuan_year.ACTUAL_MONEY,0)-ifnull(cost_year.cost_AMOUNT,0)-ifnull(workload_year.workday_cost,0))/ifnull(workload_year.workday,0)  end as actual_per_capita_income, -- 本年创收
case when ifnull(workload_all.workday,0)<=0 then 0 else  (ifnull(shoukuan_all.ACTUAL_MONEY,0)-ifnull(cost_all.cost_AMOUNT,0)-ifnull(workload_all.workday_cost,0))/ifnull(workload_all.workday,0)  end as actual_per_capita_income_pjt, -- 项目创收
pjt.pjt_sts,
null as deputy_leader,
pjt.initiation_date,
pjt.initiation_status,
pjt.contract_status,
ifnull(schedule1.milestone_code,'--') as milestone_code,-- 需要数据探源取erp数据
ifnull(schedule1.milestone_name,'--') as milestone_name,-- 需要数据探源取erp数据

-- 1 项目未结算的
case when pjt.pjt_sts in('1','3') then (
    -- 信息化开发、实施项目
    case when project_type.project_type_name in('信息化开发','信息化实施') then (
		    case when project_info.total_invoiced_ratio < 0.9 then (
				   
					 case when schedule2.pjt_code is not null then (
					 
					 case when year_info.project_progress<90 then
					    
					
					
					case when now()>pjt.planned_end_date then '异常' 
					 when datediff(now(),pjt.PLANNED_START_DATE)>150 and pjt.Initiation_status='预立项' then '异常'
					else	
	          (
						case when schedule1.is_postpone ='Y' then '异常' 
                 when schedule1.is_postpone ='N' then 
								 ( 
								     case when schedule13.pjt_code is not null then '异常' else '正常' end
							 )
		          else '异常'  end
						)	end				
		      
					else '正常' end
					
					) else '正常' end
		    
				
				
				) else '正常' end		
		) else '正常' end		
) else '正常' end as status,




case when pjt.pjt_sts in('1','3') then (
    -- 信息化开发、实施项目
    case when project_type.project_type_name in('信息化开发','信息化实施') then (
		    case when project_info.total_invoiced_ratio < 0.9 then (
				   
					 case when schedule2.pjt_code is not null then (
					 
					 case when year_info.project_progress<90 then
					    
					
					
					case when now()>pjt.planned_end_date  then '超出合同周期' 
					     when datediff(now(),pjt.PLANNED_START_DATE)>150 and pjt.Initiation_status='预立项' then '未转正式立项超期'
							 
					else	
	          (
						case when schedule1.is_postpone ='Y' then '里程碑进度异常'
                 when schedule1.is_postpone ='N' then 
								 ( 
								     case when schedule13.pjt_code is not null then '里程碑进度异常' else '' end
							 )
		          else '里程碑进度异常'  end
						)	end				
		      
					else null end
					
					) else null end
		    
				
				
				) else null end		
		) else null end		
) else null end as Progress_Excep_Des,

-- case when pjt.pjt_sts in('1','3') then (
--     -- 信息化开发、实施项目
--     case when project_type.project_type_name in('信息化开发','信息化实施') then (
-- 		    case when project_info.total_invoiced_ratio < 0.9 then (    
-- 				    case when schedule2.pjt_code is not null then ( 
-- 					
-- 					case when now()>pjt.planned_end_date then '超出合同周期' else	
-- 	          (case when schedule1.is_postpone ='Y' then '里程碑进度异常' 
--                   when schedule1.is_postpone ='N' then null
-- 		              else '里程碑进度异常'  end
-- 						)	end				
-- 		      ) else null end
-- 		    ) else null end		
-- 		) else null end		
-- ) else null end  as Progress_Excep_Des,


null as progress_ratio,-- 需要数据探源取erp数据
null as progress_des,-- 需要数据探源取erp数据
((case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end)) as planned_cost_amt,
(ifnull(budgetcost.RENLI_AMOUNT,0)) as planned_cost_labor,
(ifnull(budgetcost.purchase_planned_cost_amt,0)) as planned_cost_procurement,
(ifnull(budgetcost.run_planned_cost_amt,0)) as planned_cost_run,
(ifnull(budgetcost.SHUIJIN_AMOUN,0)+ifnull(budgetcost.YINHUASHUI_AMOUN,0)) as planned_cost_tax,
(ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0)) as actual_cost_amt,
(ifnull(workload.workday,0)) as actual_cost_labor,
(ifnull(budgetcost.purchase_actual_cost_amt,0)) as actual_cost_procurement,
(ifnull(budgetcost.run_actual_cost_amt,0))  as actual_cost_run,
null as actual_cost_tax,
(round(((ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0))/((case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end)))*100,2)) as cost_progress,


 



-- 1 项目未结算的
case when pjt.pjt_sts in('1','3') then (
    -- 信息化开发、实施项目
		    case when project_info.total_invoiced_ratio < 0.9 then (				 
					 case when year_info.project_progress<90 then							
		      
					    (
							
							case when (((case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end))-(ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0)))>0 then 0 else abs(((case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end))- (ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0))) end
							
							
							)
					
					
					else 0 end
					
				) else 0 end		
) else 0 end as  overruns_cost_amt,





ifnull(receipt1.scheming_receipts_money,0) as planned_collection_amt,
ifnull(receipt1.actual_money,0) as actual_collection_amt,
case when (ifnull(receipt1.scheming_receipts_money,0)-ifnull(receipt1.actual_money,0))>0 then (ifnull(receipt1.scheming_receipts_money,0)-ifnull(receipt1.actual_money,0)) else 0 end as deferred_collection_amt,
null as cost_anomaly_des,
null as deferred_collection_des,
null as cumulativeinvoicing,
null as invoicedbutunpaidamount,
null as expected_receipt_date,
null as expected_receipt_desc,
ifnull(pjt.CONTRACT_MONEY,0)-(case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end) as planned_profit,
round(((ifnull(pjt.CONTRACT_MONEY,0)-(case when pjt.is_yanfa='1' then pjt.CONTRACT_MONEY else ifnull(budgetcost.cost_all_amt,0) end))/ifnull(pjt.CONTRACT_MONEY,0))*100,2) as planned_profit_margin,
(ifnull(pjt.CONTRACT_MONEY,0)-(ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0))) as current_actual_profit,
round(((ifnull(pjt.CONTRACT_MONEY,0)-(ifnull(budgetcost.purchase_actual_cost_amt,0)+ifnull(budgetcost.run_actual_cost_amt,0)+ifnull(workload.workday,0)))/ifnull(pjt.CONTRACT_MONEY,0))*100,2) as current_actual_profit_margin,
null as realized_profit,
null as settleable_profit,
null as expected_settleable_profit,
null as expected_settleable_date,
pjt.CONTRACT_SIGN_DATE,

case when pjt.pjt_sts in('1','3') then (  case when pjt.planned_start_date is not null  and pjt.planned_end_date is not null then    round(  (DATEDIFF(now(),pjt.planned_start_date) /DATEDIFF(pjt.planned_end_date,pjt.planned_start_date))*100 ,2) 	else null end) else 100 end as ContractProgress_rate,
null as rdmProgress_rate,
ifnull(budgetcost.RENLI_AMOUNT,0) as actual_cost_renli,
(ifnull(budgetcost.SHUIJIN_AMOUN,0)+ifnull(budgetcost.YINHUASHUI_AMOUN,0)) as actual_cost_shuijin,
caSE WHEN IFNULL(pjt.CONTRACT_MONEY,0)>0 THEN round((ifnull(receipt1.actual_money,0)/ifnull(pjt.CONTRACT_MONEY,0))*100,2) else 0 end  as actual_collection_rate,
ifnull(receipt_year.receipt_MONEY,0) as actual_collection_current,
ifnull(yingyu1.incexp_amount,0) as incexp_amount_current,
ifnull(yingyu1.incexp_amount_pro,0) AS incexp_amount,
case when (ifnull(receipt1.scheming_receipts_money,0)-ifnull(receipt1.actual_money,0))>0 and receipt_detail_max.scheming_receipts_date is not null then DATEDIFF(now(),receipt_detail_max.scheming_receipts_date) else 0 end AS postpone_days,

ifnull(workload.rdm_workday,0) as actual_workdays,
ifnull(members_p2.WORKINGR_DAY_P,0) as  planned_workdays,
case when ifnull(members_p2.WORKINGR_DAY_P,0)>0 then  round((ifnull(workload.rdm_workday,0)/ifnull(members_p2.WORKINGR_DAY_P,0))*100,2) else 0 end as workdays_rate,


case when (case when ifnull(members_p2.WORKINGR_DAY_P,0)>0 then  round((ifnull(workload.rdm_workday,0)/ifnull(members_p2.WORKINGR_DAY_P,0))*100,2) else 0 end)>=100 then 'Y' else 'N' end as HumanExc_status,
case when (case when ifnull(members_p2.WORKINGR_DAY_P,0)>0 then  round((ifnull(workload.rdm_workday,0)/ifnull(members_p2.WORKINGR_DAY_P,0))*100,2) else 0 end)>=100 then '人天超计划' else null end as HumanExc_desc,
receipt_detail_min.scheming_receipts_date as next_receipt_date,


income.income_amount,
income.accounts_receivable,
case when workday_sum1.workday_year>0 then (income.income_amount / workday_sum1.workday_year) else income.income_amount end as per_capita_revenue,
case when workday_sum1.workday_year>0 then (income.net_profit_amount / workday_sum1.workday_year) else income.net_profit_amount end as per_capita_profit,
profit.profit_amt,
income.net_profit_amount,

income.total_income_amount,
income.total_accounts_receivable,
case when workday_sum2.workday_year>0 then (income.total_income_amount / workday_sum2.workday_year) else income.total_income_amount end as total_per_capita_revenue,
case when workday_sum2.workday_year>0 then (income.total_net_profit_amount / workday_sum2.workday_year) else income.total_net_profit_amount end as total_per_capita_profit,
ifnull(pjt.CONTRACT_MONEY,0)-ifnull(budgetcost.COST_BUDGET_AMT,0) as total_profit_amt,
income.total_net_profit_amount as total_net_profit_amt,


target.confirmed_revenue as plan_income_amount,
target.accounts_receivable as plan_accounts_receivable,
target.per_capita_revenue as plan_per_capita_revenue,
target.per_capita_profit as plan_per_capita_profit,
target.profit_amt as plan_profit_amt,
target.confirmed_net_profit as plan_net_profit_amount,



target.total_confirmed_revenue as plan_total_income_amount,
0 as plan_total_accounts_receivable,
target.total_per_capita_revenue as plan_total_per_capita_revenue,
target.total_per_capita_profit as plan_total_per_capita_profit,
target.total_profit_amt as plan_total_profit_amt,
target.total_confirmed_net_profit_rate as plan_total_net_profit_amt,

yewu.total_bus_income_amount,
yewu.total_bus_accounts_receivable,
yewu.total_bus_per_capita_revenue,
yewu.total_bus_per_capita_profit,
yewu.total_bus_profit_amt,
yewu.total_bus_net_profit_amt




from dm_pjt_pjtinfo pjt

left join yingyu yingyu1 on yingyu1.pjt_code=pjt.pjt_code



left join (
    select * from dm_pjt_schedule1 where is_current='Y'
) schedule1 on schedule1.pjt_code = pjt.pjt_code

left join (
    select pjt_code from dm_pjt_schedule1 group by pjt_code
) schedule2 on schedule2.pjt_code = pjt.pjt_code
left join(

select schedule1a.pjt_code from (
    SELECT pjt_code 
    FROM dm_pjt_schedule1 
    WHERE  end_time is null and plan_end_time is null
    group by pjt_code
)schedule1a
left join(

    select pjt_code
    from dm_pjt_schedule1
    where end_time is not null or plan_end_time is not null
    group by pjt_code
)schedule1b on schedule1a.pjt_code=schedule1b.pjt_code
where schedule1b.pjt_code is null


)schedule13 on schedule13.pjt_code = pjt.pjt_code






left join dm_pjt_budgetcost budgetcost on budgetcost.pjt_id =pjt.pjt_id  
left join(
    select  
		    pjt_id,
		    sum(scheming_receipts_money) as scheming_receipts_money,
				sum(actual_money) as actual_money 
		from dm_pjt_receipt 
		group by pjt_id
)receipt1 on receipt1.pjt_id =pjt.pjt_id
left join(
  select
		pjt_code,
		sum(ifnull(pjt_workload.workday,0)) as rdm_workday,
		sum(ifnull(pjt_workload.workday,0)*ifnull(priceday.PriceDay,1800)) as workday 
		from dm_pjt_workload pjt_workload
		left join dm_pjt_priceday priceday on priceday.Num=pjt_workload.member_code
		group by pjt_workload.pjt_code 

)workload on workload.pjt_code=pjt.pjt_code

left join( 
    select 
    workload21.pjt_code,
    count(*) as item_members,
    sum(case when  workload22.avgeffort<6 then 1 else 0 end) as un_saturated
    from
    (
        select
    		    pjt_code,
    				member_name
    		from dm_pjt_workload
    		group by pjt_code,member_name
    )workload21
    left join 
    (
        select 
    				member_name,
    				round(sum(effort)/count(distinct date_format(reportday, '%y-%m-%d')),1) as avgeffort
    		from dm_pjt_workload
    		where reportday>=date_sub(now(), interval 6 month)  and
              reportday<=now()
    		group by member_name
    )workload22 on workload21.member_name=workload22.member_name
    group by workload21.pjt_code
)workload2 on workload2.pjt_code=pjt.pjt_code
LEFT JOIN (

    SELECT Psm_Pjt.code,Psm_Pjt.RealStartDate,
    Psm_Pjt.RealEndDate, phase.name FROM t_Psm_Pjt Psm_Pjt
    LEFT JOIN t_lc_phase phase on Psm_Pjt.PhaseID=phase.id
) Psm_Pjt2 ON Psm_Pjt2.code=pjt.pjt_id
left join(
    select
    members_p1.pjt_id,
		sum(ifnull(WORKINGR_DAY,0)) as WORKINGR_DAY_P,
    sum(ifnull(WORKINGR_DAY,0))/250 as WORKINGR_DAY
    from  dm_pjt_members_p members_p1
    group by
    members_p1.pjt_id
)members_p2 on members_p2.pjt_id =pjt.pjt_id  


left join (

select 
workload1.pjt_code,
year(workload1.reportday) as statyear,
sum(ifnull(workload1.workday,0))/250 as workday,
sum(ifnull(workload1.workday,0))*1800 as workday_cost
from  dm_pjt_workload workload1
where year(workload1.reportday)=year(now())
group by
workload1.pjt_code,
year(workload1.reportday)

)workload_year on workload_year.pjt_code=pjt.pjt_code

left join(
select 
workload1.pjt_code,
sum(ifnull(workload1.workday,0))/250 as workday,
sum(ifnull(workload1.workday,0))*1800 as workday_cost
from  dm_pjt_workload workload1
group by
workload1.pjt_code

)workload_all on workload_all.pjt_code=pjt.pjt_code


left join (
select
    receipt_a.pjt_id,
    year(receipt_a.ACTUAL_DATE) as statyear,
    sum(ifnull(receipt_a.ACTUAL_MONEY,0)) as ACTUAL_MONEY
from dm_pjt_receipt_detail_a receipt_a
where year(receipt_a.ACTUAL_DATE)=year(now())
group by
    receipt_a.pjt_id,
    year(receipt_a.ACTUAL_DATE)

)shoukuan_year on shoukuan_year.pjt_id =pjt.pjt_id

left join (
select
    receipt_a.pjt_id,
    sum(ifnull(receipt_a.ACTUAL_MONEY,0)) as ACTUAL_MONEY
from dm_pjt_receipt_detail_a receipt_a
group by
    receipt_a.pjt_id

)shoukuan_all on shoukuan_all.pjt_id =pjt.pjt_id


left join (
select
    costdetail.pjt_id,
    year(costdetail.EFFECTIVE_DATE) as statyear,
    sum(ifnull(costdetail.AMOUNT,0)) as cost_AMOUNT
from dm_pjt_costdetail costdetail
where year(costdetail.EFFECTIVE_DATE)=year(now())
group by
    costdetail.pjt_id,
    year(costdetail.EFFECTIVE_DATE)

)cost_year on cost_year.pjt_id =pjt.pjt_id

left join (
select
    costdetail.pjt_id,
    sum(ifnull(costdetail.AMOUNT,0)) as cost_AMOUNT
from dm_pjt_costdetail costdetail
group by
    costdetail.pjt_id
		
)cost_all on cost_all.pjt_id =pjt.pjt_id
left join (


    select receipt_a.pjt_code,
           receipt_a.pjt_name,
           sum(ifnull(receipt_a.ACTUAL_MONEY, 0)) as receipt_MONEY
    from dm_pjt_receipt_detail_a receipt_a
             left join t_cd_dataweek dataweek
                       on DATE(receipt_a.ACTUAL_DATE) = DATE(dataweek.calendr_dte)
	 where year(receipt_a.ACTUAL_DATE)=year(now())
    group by receipt_a.pjt_code, receipt_a.pjt_name
)receipt_year on receipt_year.pjt_code =pjt.pjt_code

left join(

select detail_p.pjt_code,
max(detail_p.scheming_receipts_date) as scheming_receipts_date
from  dm_pjt_receipt_detail_p detail_p 
where detail_p.SCHEMING_RECEIPTS_DATE<=now()
group by detail_p.pjt_code

)receipt_detail_max on receipt_detail_max.pjt_code =pjt.pjt_code

left join(

select detail_p.pjt_code,
min(detail_p.scheming_receipts_date) as scheming_receipts_date
from  dm_pjt_receipt_detail_p detail_p 
where detail_p.SCHEMING_RECEIPTS_DATE>now()
group by detail_p.pjt_code

)receipt_detail_min on receipt_detail_min.pjt_code =pjt.pjt_code
left join(

SELECT
	   contra.pjt_code,
		sum( ifnull( contra.current_year_confirmed_revenue, 0 ) ) AS income_amount,
		sum( ifnull( contra.accounts_receivable, 0 ) ) AS accounts_receivable,
		sum( ifnull( contra.current_year_confirmed_net_profit, 0 ) ) AS net_profit_amount,
		
		sum( ifnull( contra.total_confirmed_revenue, 0 ) ) AS total_income_amount,
		sum( ifnull( contra.total_confirmed_revenue, 0 ) - ifnull( contra.total_received_amount, 0 )) AS total_accounts_receivable,
		sum( ifnull( contra.total_confirmed_net_profit, 0 ) ) AS total_net_profit_amount
		
	FROM
		dm_pjt_financial_info contra
	WHERE
		STAT_YEAR=year(now())
		group by contra.pjt_code

)income on income.pjt_code =pjt.pjt_code

left join(

SELECT
	   contra.pjt_code,
		sum( ifnull( contra.profit_amt, 0 ) ) AS profit_amt
		
	FROM
		dm_pjt_financial_profit contra
	WHERE
		STAT_YEAR=year(now())
		group by contra.pjt_code

)profit on profit.pjt_code =pjt.pjt_code
left join workday_sum workday_sum1 on  workday_sum1.pjt_code =pjt.pjt_code
left join dm_pjt_financial_target target on target.pjt_code=pjt.pjt_code
left join total_workday_sum workday_sum2 on  workday_sum2.pjt_code =pjt.pjt_code
left join(
select pjt_code,project_type_name 
from dm_pjt_financial_info 
group by pjt_code,project_type_name
)project_type on  project_type.pjt_code =pjt.pjt_code

left join(
    select info3.* from (
select info2.pjt_code,info2.total_invoiced_ratio,row_number() over(PARTITION by info2.pjt_code order by info2.stat_day desc) as sn from dm_pjt_financial_info info2
)info3 where info3.sn=1
)project_info on  project_info.pjt_code =pjt.pjt_code
left join(
select pjt_code,project_progress from dm_pjt_financial_info_year WHERE
		STAT_YEAR=year(now())

)year_info on  year_info.pjt_code =pjt.pjt_code
left join(
select
financial1.pjt_code,
financial1.pjt_name,
financial1.CONTRACT_MONEY,
financial1.project_progress,
round((financial1.CONTRACT_MONEY/1.06)*ifnull(financial1.project_progress,0),2) as total_bus_income_amount, -- 收入
detail_p1.scheming_receipts_money-detail_a1.ACTUAL_MONEY as total_bus_accounts_receivable, -- 应收账款
case when (round(workday_toal.workday_year/450000,2))=0 then 0 else 
round(((financial1.CONTRACT_MONEY/1.06)*ifnull(financial1.project_progress,0))/(round(workday_toal.workday_year/450000,2)),2) end as total_bus_per_capita_revenue,

((financial1.CONTRACT_MONEY/1.06)*ifnull(financial1.project_progress,0))-(budgetcost1.COST_BUDGET_AMT*ifnull(financial1.project_progress,0))-(ifnull(workday_toal.workday_year,0)) as total_bus_net_profit_amt,
case when (round(workday_toal.workday_year/450000,2))=0 then 0 else 
round((((financial1.CONTRACT_MONEY/1.06)*ifnull(financial1.project_progress,0))-(budgetcost1.COST_BUDGET_AMT*ifnull(financial1.project_progress,0))-(ifnull(workday_toal.workday_year,0)))/(round(workday_toal.workday_year/450000,2)),2) end as total_bus_per_capita_profit,
financial1.CONTRACT_MONEY-budgetcost1.COST_BUDGET_AMT as total_bus_profit_amt
from financial_yewu financial1
left join total_workday_sum_yewu workday_toal on workday_toal.pjt_code=financial1.pjt_code
left join dm_pjt_budgetcost budgetcost1 on budgetcost1.pjt_code=financial1.pjt_code
left join(

select
    detail_p.pjt_code,
    sum(ifnull(detail_p.scheming_receipts_money,0)) as scheming_receipts_money
 from  dm_pjt_receipt_detail_p detail_p 
 where detail_p.scheming_receipts_date<now()
 group by detail_p.pjt_code

)detail_p1 on detail_p1.pjt_code=financial1.pjt_code
left join(
select
detail_a.pjt_code,
sum(ifnull(detail_a.ACTUAL_MONEY,0)) as ACTUAL_MONEY
from dm_pjt_receipt_detail_a detail_a
GROUP BY
detail_a.pjt_code

)detail_a1  on detail_a1.pjt_code=financial1.pjt_code

)yewu on yewu.pjt_code =pjt.pjt_code