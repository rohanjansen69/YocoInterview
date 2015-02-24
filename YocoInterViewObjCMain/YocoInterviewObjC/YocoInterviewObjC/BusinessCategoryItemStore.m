//
//  BusinessCategoryItemStore.m
//  YocoInterviewObjC
//
//  Created by Rohan Jansen on 2015/02/12.
//  Copyright (c) 2015 Rohan Jansen. All rights reserved.
//

#import "BusinessCategoryItemStore.h"

@implementation BusinessCategoryItemStore

+ (instancetype) sharedStore
{
    static BusinessCategoryItemStore *sharedStore = nil;
    
    if(!sharedStore) {
        sharedStore = [[self alloc] initPrivate];
    }
    
    return sharedStore;
}

-(instancetype) initPrivate
{
    self = [super init];
    
    if(self) {
        _privateItems = [[NSMutableArray alloc] init];
    }
    
    return self;
}

-(instancetype) init
{
    @throw [NSException exceptionWithName:@"SingleTon" reason:@"Use +BusinessCategoryItemStore sharedStore" userInfo:nil];
}

- (BusinessCategory *) createItem: (NSString *)name withCategory:(NSString *)category
{
    BusinessCategory *item = [[BusinessCategory alloc] init];
    item.Name = name;
    item.Category = category;
    
    [self.privateItems addObject:item];
    
    return item;
}

- (NSArray *) allItems
{
    return self.privateItems;
}

@end
