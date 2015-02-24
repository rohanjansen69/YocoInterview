//
//  BusinessCategoriesViewController.m
//  YocoInterviewObjC
//
//  Created by Rohan Jansen on 2015/02/12.
//  Copyright (c) 2015 Rohan Jansen. All rights reserved.
//

#import "BusinessCategoriesViewController.h"
#import "RestService.h"
#import "BusinessCategoryItemStore.h"
#import "NSArray+LinqExtensions.h"
#import "NSDictionary+LinqExtensions.h"

@implementation BusinessCategoriesViewController 

- (instancetype) init
{
    self = [super initWithStyle:UITableViewStylePlain];
    
    if(self) {
        [self updateViewWithLatestData];
    }
    
    return self;
}

- (instancetype) initWithStyle:(UITableViewStyle)style
{
    return [self init];
}

-(void) viewDidLoad
{
    [super viewDidLoad];
    
    [self.tableView registerClass:[UITableViewCell class] forCellReuseIdentifier:@"UITableViewCell"];
    
    self.tableView.contentInset = UIEdgeInsetsMake(44, 0, 0, 0);
    
    self.tableView.delegate = self;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSArray *items = [self getItemsArrayForGroupAt:indexPath];
    
    BusinessCategory *item = [items objectAtIndex:indexPath.row];
    
    UIAlertView *messageAlert = [[UIAlertView alloc]
                                 initWithTitle:@"Row Selected"
                                 message:[NSString stringWithFormat:@"You've selected %@", item.Name]
                                 delegate:nil
                                 cancelButtonTitle:@"OK"
                                 otherButtonTitles:nil];
    
    // Display Alert Message
    [messageAlert show];
    
    [tableView deselectRowAtIndexPath:indexPath animated:YES];
}

- (NSString *) tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section
{
    NSArray *allKeys = [self allKeysForGroupedCategory];
    
    NSString *sectionTitle = [allKeys objectAtIndex:section];
    
    return sectionTitle;
}

- (NSArray *)getItemsArrayForGroupAt:(NSIndexPath *)indexPath
{
    NSArray *allKeys = [self allKeysForGroupedCategory];
    
    NSString *sectionTitle = [allKeys objectAtIndex:indexPath.section];
    
    NSArray *items = [[self getGroupedCategories] valueForKey:sectionTitle];
    
    return items;
}

- (UITableViewCell*) tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"UITableViewCell"];
    
    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:@"UITableViewCell"];
    }
    
    NSArray *items;
    items = [self getItemsArrayForGroupAt:indexPath];
    
    NSInteger count = [items count] - 1;
    NSInteger rowCount = indexPath.row;
    
    if(rowCount <= count) {
        BusinessCategory *item = [items objectAtIndex:indexPath.row];
        cell.textLabel.text = item.Name;
    }
   
    return cell;
}


-(NSInteger) numberOfSectionsInTableView:(UITableView *)tableView
{
    NSArray *allKeys = [self allKeysForGroupedCategory];
    
    return [allKeys count];
}

- (NSInteger) tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    NSArray *allKeys = [self allKeysForGroupedCategory];
    
    NSString *sectionTitle = [allKeys objectAtIndex:section];
    
    NSArray *items = [[self getGroupedCategories] valueForKey:sectionTitle];
    
    return [items count];
}

- (NSArray *)getArchivedObjectArray
{
    NSData *data = [[NSUserDefaults standardUserDefaults] objectForKey:@"cache"];
    
    NSArray *savedArray = [NSKeyedUnarchiver unarchiveObjectWithData:data];
    
    return savedArray;
}

-(NSDictionary *) getGroupedCategories
{
    NSArray *savedArray;
    savedArray = [self getArchivedObjectArray];
    
    NSDictionary* groupdByCategory = [savedArray linq_groupBy:^id(id item) {
        BusinessCategory *obj = (BusinessCategory *)item;
        return obj.Category;
    }];
    
    //TODO: debug code, remove when done
    [groupdByCategory enumerateKeysAndObjectsUsingBlock:^(id key, id obj, BOOL *stop) {
        NSLog(@"%@:%@",key, obj);
    }];
    
    return groupdByCategory;
}

-(NSArray *) allKeysForGroupedCategory
{
    NSDictionary *groupdByCategory = [self getGroupedCategories];
    
    NSArray *allKeys = [groupdByCategory allKeys];
    
    return allKeys;
}

-(void) updateViewWithLatestData
{
    
    NSString *businessCategoryUrl = @"http://yoco-core-staging.herokuapp.com/api/common/v1/properties/businessCategories";
    
    RestService *service = [[RestService alloc] init];
    
    NSString *resp = [service getBusinessCategories:businessCategoryUrl];
    
    NSError *error = nil;
    
    NSData *data = [resp dataUsingEncoding:NSUTF8StringEncoding];
    
    //get the json into a foundation object
    id object = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&error];
    
    if([object isKindOfClass:[NSDictionary class]] && error == nil)
    {
        NSLog(@"Dictionary object: %@", object);
        
        NSArray *array;
        // Get the 'data' array
        if ([[object objectForKey:@"data"] isKindOfClass:[NSArray class]])
        {
            array = [object objectForKey:@"data"];
            NSLog(@"results array: %@", array);
        }
        
        for(int i = 0; i < [array count]; i++)
        {
            NSArray *resultOBjects = [array objectAtIndex:i];
            NSLog(@"results resultOBjects: %@", resultOBjects);
            
           [[BusinessCategoryItemStore sharedStore] createItem:[resultOBjects objectAtIndex:1] withCategory:[resultOBjects objectAtIndex:2]];
        }
        
        NSArray *items = [[BusinessCategoryItemStore sharedStore] allItems];
        
        NSData *dataSave = [NSKeyedArchiver archivedDataWithRootObject:items];
        
        [[NSUserDefaults standardUserDefaults] setObject:dataSave forKey:@"cache"];
        [[NSUserDefaults standardUserDefaults] synchronize];
        
        [self.tableView reloadData];
    }
}

@end
