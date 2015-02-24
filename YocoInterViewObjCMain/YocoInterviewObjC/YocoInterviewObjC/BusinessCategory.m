//
//  BusinessCategory.m
//  YocoInterviewObjC
//
//  Created by Rohan Jansen on 2015/02/12.
//  Copyright (c) 2015 Rohan Jansen. All rights reserved.
//

#import "BusinessCategory.h"

@implementation BusinessCategory

@synthesize Id;
@synthesize Category;
@synthesize Name;

#pragma - Encoder

- (id)initWithCoder:(NSCoder *)decoder {
    self = [super init];
    if (!self) {
        return nil;
    }
    
    self.Id = [decoder decodeObjectForKey:@"Id"];
    self.Category = [decoder decodeObjectForKey:@"Category"];
    self.Name = [decoder decodeObjectForKey:@"Name"];
    
    return self;
}

- (void)encodeWithCoder:(NSCoder *)encoder {
    [encoder encodeObject:self.Id forKey:@"Id"];
    [encoder encodeObject:self.Category forKey:@"Category"];
    [encoder encodeObject:self.Name forKey:@"Name"];
}

@end
